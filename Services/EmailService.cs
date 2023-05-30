using System.Net;
using MovieBooking.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MovieBooking.Services 
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendBookingConfirmationEmail(string recipient, Booking booking)
        {
            var apiKey = _configuration["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("noreply@moviebooking.com", "Movie Booking");
            var to = new EmailAddress(recipient);
            var subject = "Booking Confirmation";
            var plainTextContent = $"Hi,\n\nYou have successfully booked {booking.Tickets} tickets for the movie '{booking.Movie.Title}' on {booking.BookingDate}.\n\nThank you for choosing Movie Booking!";
            var htmlContent = $"<p>Hi,</p><p>You have successfully booked {booking.Tickets} tickets for the movie '{booking.Movie.Title}' on {booking.BookingDate}.</p><p>Thank you for choosing Movie Booking!</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != HttpStatusCode.Accepted && response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Failed to send email. StatusCode={response.StatusCode}");
            }
        }
    }

}