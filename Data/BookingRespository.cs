using Microsoft.EntityFrameworkCore;
using MovieBooking.Models;

namespace MovieBooking.Data
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;
        private readonly IMovieRepository _movieRepository;

        public BookingRepository(AppDbContext context, IMovieRepository movieRepository)
        {
            _context = context;
            _movieRepository = movieRepository;
        }

        public async Task CreateBooking(Booking booking)
        {
            var movie = await _movieRepository.GetMovieById(booking.MovieId);

            if (movie == null)
            {
                throw new Exception($"Movie with id {booking.MovieId} not found");
            }

            movie.AvailableSeats -= booking.Tickets;

            if (movie.AvailableSeats < 0)
            {
                throw new Exception($"Not enough seats available for movie {movie.Title}");
            }

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }


        public async Task<Booking> BookMovie(int movieId, int userId)
        {
            var movie = await _movieRepository.GetMovieById(movieId);

            if (movie == null)
            {
                throw new Exception($"Movie with id {movieId} not found");
            }

            var booking = new Booking
            {
                MovieId = movieId,
                UserId = userId,
                BookingDate = DateTime.Now
            };

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task<Booking> GetBookingById(int id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Booking[]> GetBookingsByUser(int userId)
        {
            return await _context.Bookings.Where(x => x.UserId == userId).ToArrayAsync();
        }
    }
}
