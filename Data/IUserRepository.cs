using MovieBooking.Models;

namespace MovieBooking.Data
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<User> AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        bool UserExists(int id);
        bool UserExistsEmail(string email);
    }
}
