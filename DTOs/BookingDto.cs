namespace MovieBooking.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; }
        public DateTime BookingTime { get; set; }
        public int Tickets { get; set; }
    }
}