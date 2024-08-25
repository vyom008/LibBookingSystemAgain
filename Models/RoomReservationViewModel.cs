using System;
using System.Collections.Generic;

namespace LibBooking.Models
{
    public class RoomReservationViewModel
    {
        public DateTime Date { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
    public enum TimeSlotStatus
    {
        Default,    // 灰色，可管理的
        Blocked,    // 红色，不可选的
        Available   // 绿色，用户可选的
    }

}
