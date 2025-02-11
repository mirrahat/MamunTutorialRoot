using System.ComponentModel.DataAnnotations;

namespace MamunTutorial.Models
{
    public class Students
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string RollNumber { get; set; }

        [Required]
        public string ClassName { get; set; }

        // New fields
        [Required]
        [Phone] // Validation for phone number
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress] // Validation for email address
        public string Email { get; set; }
    }
}
