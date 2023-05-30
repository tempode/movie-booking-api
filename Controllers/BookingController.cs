using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieBooking.Data;
using MovieBooking.DTOs;
using MovieBooking.Models;

namespace MovieBooking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public BookingController(IBookingRepository bookingRepository, IMovieRepository movieRepository, IUserRepository userRepository, IEmailService emailService)
        {
            _bookingRepository = bookingRepository;
            _movieRepository = movieRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> CreateBooking(BookingCreateDto bookingCreateDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var user = await _userRepository.GetUserById(userId);

            if (user == null)
            {
                return Unauthorized();
            }

            var movie = await _movieRepository.GetMovieById(bookingCreateDto.MovieId);

            if (movie == null)
            {
                return BadRequest("Invalid movie ID");
            }

            if (movie.AvailableSeats < bookingCreateDto.Tickets)
            {
                return BadRequest("Not enough seats available for this movie");
            }

            var booking = new Booking
            {
                UserId = userId,
                MovieId = bookingCreateDto.MovieId,
                Tickets = bookingCreateDto.Tickets
            };

            await _bookingRepository.CreateBooking(booking);

            movie.AvailableSeats -= bookingCreateDto.Tickets;

                        await _movieRepository.UpdateMovie(movie);

            // Send confirmation email to user
            await _emailService.SendBookingConfirmationEmail(user.Email, booking);

            return new BookingDto
            {
                Id = booking.Id,
                MovieTitle = movie.Title,
                BookingTime = booking.BookingDate,
                Tickets = booking.Tickets
            };
        }

        [HttpGet]
        public async Task<ActionResult<BookingDto[]>> GetBookings()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var user = await _userRepository.GetUserById(userId);

            if (user == null)
            {
                return Unauthorized();
            }

            var bookings = await _bookingRepository.GetBookingsByUser(userId);

            var bookingDtos = new BookingDto[bookings.Length];

            for (var i = 0; i < bookings.Length; i++)
            {
                var booking = bookings[i];

                var movie = await _movieRepository.GetMovieById(booking.MovieId);

                bookingDtos[i] = new BookingDto
                {
                    Id = booking.Id,
                    MovieTitle = movie.Title,
                    BookingTime = booking.BookingDate,
                    Tickets = booking.Tickets
                };
            }

            return bookingDtos;
        }
    }
}

