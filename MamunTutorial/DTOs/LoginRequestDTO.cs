using System.ComponentModel.DataAnnotations;

namespace MamunTutorial.DTOs
{
    public class LoginRequestDTO
    {
        [Required]
        public string Identifier { get; set; }  // Can be either email or phone number

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
    }
}
