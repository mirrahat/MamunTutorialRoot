using System;
using System.ComponentModel.DataAnnotations;

namespace MamunTutorial.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } // Primary Key


        [Required]
        public string Password { get; set; } // Encrypted Password

        [Required]
        public string Role { get; set; } // Role: "Student" or "Teacher"

        [Required]
        [EmailAddress]
        public string Email { get; set; } // Email Address (For dynamic validation)

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } // Phone Number (For dynamic validation)

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpirationDateTime { get; set; }
    }
}
