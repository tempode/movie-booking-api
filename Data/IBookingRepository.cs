using MovieBooking.Models;

namespace MovieBooking.Data
{
    public interface IBookingRepository
    {
        Task<Booking> BookMovie(int movieId, int userId);
        Task CreateBooking(Booking booking);
        Task<Booking> GetBookingById(int id);
        Task<Booking[]> GetBookingsByUser(int userId);
    }
}
