using LibBooking.Data;
using LibBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations([FromQuery] int? roomID, [FromQuery] DateTime? start, [FromQuery] DateTime? end)
        {
            var query = _context.Reservations.AsQueryable();

            if (roomID.HasValue)
            {
                query = query.Where(r => r.RoomID == roomID);
            }

            if (start.HasValue && end.HasValue)
            {
                query = query.Where(r => r.ReservationDate >= start && r.ReservationDate <= end);
            }

            return await query.Include(r => r.Room).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservation(ReservationDto reservationDto)
        {
            var validEmailDomains = new[] { "@student.weltec.ac.nz", "@weltec.ac.nz" };
            var emailDomain = reservationDto.Email.Substring(reservationDto.Email.IndexOf("@"));

            if (!validEmailDomains.Contains(emailDomain))
            {
                return BadRequest("Invalid email domain. Please use a valid WelTec email address.");
            }

            var reservation = new Reservation
            {
                Email = reservationDto.Email,
                RoomID = reservationDto.RoomID,
                ReservationDate = reservationDto.ReservationDate,
                StartTime = reservationDto.StartTime,
                EndTime = reservationDto.EndTime
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReservations), new { id = reservation.ID }, reservation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, Reservation reservation)
        {
            if (id != reservation.ID)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
