using MovieBooking.Models;

namespace MovieBooking.Utils
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
    }
}
