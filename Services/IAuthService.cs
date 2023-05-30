using MovieBooking.Models;
using MovieBooking.DTOs;

namespace MovieBooking.Services
{
    public interface IAuthService
    {
        Task<UserDto> Register(RegisterDto registerDto);
        Task<UserDto> Login(LoginDto loginDto);
        Task<UserDto> GenerateJwtToken(RefreshDto refreshDto);
    }
}