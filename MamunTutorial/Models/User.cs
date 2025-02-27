using System.ComponentModel.DataAnnotations;

namespace MamunTutorial.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = "Student";

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLogin { get; set; }

        [StringLength(200)]
        public string ProfilePicture { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationDateTime { get; set; }

    }
}