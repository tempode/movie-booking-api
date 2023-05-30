using MovieBooking.Models;

public interface IEmailService
{
    Task SendBookingConfirmationEmail(string email, Booking booking);
}
