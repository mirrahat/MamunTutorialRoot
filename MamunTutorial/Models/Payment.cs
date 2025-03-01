using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MamunTutorial.Models
{
    public class Payment
    {
        public Guid Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }

        [StringLength(100)]
        public string TransactionId { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Pending";

        [StringLength(50)]
        public string Month { get; set; }

        [StringLength(10)]
        public string Year { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // Navigation property
        public virtual Student Student { get; set; }
    }
}