namespace MovieBooking.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int Tickets { get; set; }
        public DateTime BookingDate { get; set; }
        public Movie Movie { get; set; }
    }
}
