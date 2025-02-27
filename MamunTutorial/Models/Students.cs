using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MamunTutorial.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        [StringLength(50)]
        public string Status { get; set; } = "Active";

        [StringLength(500)]
        public string Notes { get; set; }

        // Navigation property
        public virtual User User { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

        public Student()
        {
            Payments = new HashSet<Payment>();
        }
    }
}