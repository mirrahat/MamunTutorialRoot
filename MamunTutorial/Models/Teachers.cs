using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MamunTutorial.Models
{
    public class Teachers
    {      
            [Key]
            public Guid TeacherId { get; set; } // Primary Key
            public string Name { get; set; } // Name of the Teacher
            public string Email { get; set; } // Email Address
            public string PhoneNumber { get; set; } // Contact Number

    }
}
