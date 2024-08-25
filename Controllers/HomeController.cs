using LibBooking.Data;
using LibBooking.Models;
using LibBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public HomeController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index(DateTime? date)
        {
            if (date == null)
            {
                date = DateTime.Today;
            }

            var rooms = await _context.Rooms.ToListAsync();
            var reservations = await _context.Reservations
                .Where(r => r.ReservationDate == date)
                .ToListAsync();

            var model = new RoomReservationViewModel
            {
                Date = date.Value,
                Rooms = rooms,
                Reservations = reservations
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SubmitTime(string room, int id, int time)
        {
            Console.WriteLine($"Received time: {time}");
            // 确保 Room ID 有效
            var selectedRoom = _context.Rooms.FirstOrDefault(r => r.ID == id);
            if (selectedRoom == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Room ID.");
                return RedirectToAction("Index"); // 重定向到 Index，如果 Room ID 无效
            }

            var model = new EmailValidationViewModel
            {
                Room = room,
                ID = id,  // 使用 ID 而不是 RoomID
                Time = time,
                Date = DateTime.Today // 您可以根据需要传递日期
            };
            return View("EmailValidation", model); // 正常情况下返回到 EmailValidation 视图
        }


        [HttpPost]
        public async Task<IActionResult> ValidateEmail(EmailValidationViewModel model)
        {
            Console.WriteLine($"ValidateEmail called with Room: {model.Room}, ID: {model.ID}, Time: {model.Time}");

            var validDomains = new[] { "@student.weltec.ac.nz", "@weltec.ac.nz" };
            var emailDomain = model.Email.Substring(model.Email.IndexOf("@"));

            if (!validDomains.Contains(emailDomain))
            {
                ViewBag.Message = "提交失败，请使用学校邮箱（@student.weltec.ac.nz 或 @weltec.ac.nz）";
                Console.WriteLine("Invalid email domain.");
                return View("EmailValidation", model);
            }

            // 确保 Room ID 有效
            var selectedRoom = _context.Rooms.FirstOrDefault(r => r.ID == model.ID);
            if (selectedRoom == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Room ID.");
                Console.WriteLine("Invalid Room ID.");
                return View("EmailValidation", model);
            }

            try
            {
                // 确保正确转换时间为 TimeSpan
                var startTime = new TimeSpan(model.Time, 0, 0);
                var endTime = startTime.Add(TimeSpan.FromHours(1));

                var reservation = new Reservation
                {
                    RoomID = model.ID,
                    ReservationDate = model.Date,
                    StartTime = startTime,  // 这里使用 startTime
                    EndTime = endTime,  // 这里使用 endTime
                    Email = model.Email
                };

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
                Console.WriteLine("Reservation saved to database.");

                // 发送确认邮件
                var message = $"You have successfully booked the room {model.Room} at {model.Time}:00 on {model.Date.ToString("yyyy-MM-dd")}.";
                await _emailService.SendEmailAsync(model.Email, "Room Booking Confirmation", message);
                Console.WriteLine("Confirmation email sent.");

                ViewBag.Message = "提交成功，预订确认邮件已发送到您的邮箱。";
                return View("EmailValidation", model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"保存预订信息时发生错误: {ex.Message}";
                Console.WriteLine($"Error saving reservation: {ex.Message}");
                return View("EmailValidation", model);
            }
        }
        // Initial page
        public IActionResult Initial()
        {
            return View();
        }
    }
}
