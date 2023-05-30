// User.cs
using System.ComponentModel.DataAnnotations;

namespace MovieBooking.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string Token { get; set; }

    }
}
