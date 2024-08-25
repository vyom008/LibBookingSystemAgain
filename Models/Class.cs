namespace LibBooking.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class Room
    {
        public int ID { get; set; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
    }

    public class Reservation
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public int RoomID { get; set; }
        public Room Room { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

    public class ReservationDto
    {
        public string Email { get; set; }
        public int RoomID { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

   
}
