using MovieBooking.Models;

namespace MovieBooking.Data
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetMovies(string title);
        Task<Movie> GetMovieById(int id);
        Task UpdateMovie(Movie movie);
    }
}
