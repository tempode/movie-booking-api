using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieBooking.Data;
using MovieBooking.DTOs;

namespace MovieBooking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<MovieDto[]>> SearchMovies(string title)
        {
            var movies = await _movieRepository.GetMovies(title);

            var movieDtos = new List<MovieDto>();

            foreach (var movie in movies)
            {
                movieDtos.Add(new MovieDto
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Director = movie.Director,
                    AvailableSeats = movie.AvailableSeats
                });
            }

            return movieDtos.ToArray();
        }
    }
}
