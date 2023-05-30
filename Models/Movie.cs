namespace MovieBooking.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
