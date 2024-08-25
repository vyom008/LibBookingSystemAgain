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
            // ȷ�� Room ID ��Ч
            var selectedRoom = _context.Rooms.FirstOrDefault(r => r.ID == id);
            if (selectedRoom == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Room ID.");
                return RedirectToAction("Index"); // �ض��� Index����� Room ID ��Ч
            }

            var model = new EmailValidationViewModel
            {
                Room = room,
                ID = id,  // ʹ�� ID ������ RoomID
                Time = time,
                Date = DateTime.Today // �����Ը�����Ҫ��������
            };
            return View("EmailValidation", model); // ��������·��ص� EmailValidation ��ͼ
        }


        [HttpPost]
        public async Task<IActionResult> ValidateEmail(EmailValidationViewModel model)
        {
            Console.WriteLine($"ValidateEmail called with Room: {model.Room}, ID: {model.ID}, Time: {model.Time}");

            var validDomains = new[] { "@student.weltec.ac.nz", "@weltec.ac.nz" };
            var emailDomain = model.Email.Substring(model.Email.IndexOf("@"));

            if (!validDomains.Contains(emailDomain))
            {
                ViewBag.Message = "�ύʧ�ܣ���ʹ��ѧУ���䣨@student.weltec.ac.nz �� @weltec.ac.nz��";
                Console.WriteLine("Invalid email domain.");
                return View("EmailValidation", model);
            }

            // ȷ�� Room ID ��Ч
            var selectedRoom = _context.Rooms.FirstOrDefault(r => r.ID == model.ID);
            if (selectedRoom == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Room ID.");
                Console.WriteLine("Invalid Room ID.");
                return View("EmailValidation", model);
            }

            try
            {
                // ȷ����ȷת��ʱ��Ϊ TimeSpan
                var startTime = new TimeSpan(model.Time, 0, 0);
                var endTime = startTime.Add(TimeSpan.FromHours(1));

                var reservation = new Reservation
                {
                    RoomID = model.ID,
                    ReservationDate = model.Date,
                    StartTime = startTime,  // ����ʹ�� startTime
                    EndTime = endTime,  // ����ʹ�� endTime
                    Email = model.Email
                };

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
                Console.WriteLine("Reservation saved to database.");

                // ����ȷ���ʼ�
                var message = $"You have successfully booked the room {model.Room} at {model.Time}:00 on {model.Date.ToString("yyyy-MM-dd")}.";
                await _emailService.SendEmailAsync(model.Email, "Room Booking Confirmation", message);
                Console.WriteLine("Confirmation email sent.");

                ViewBag.Message = "�ύ�ɹ���Ԥ��ȷ���ʼ��ѷ��͵��������䡣";
                return View("EmailValidation", model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"����Ԥ����Ϣʱ��������: {ex.Message}";
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
