using System.ComponentModel.DataAnnotations;

namespace MamunTutorial.Models
{
    public class Billing
    {
        [Key]
        public Guid BillingId { get; set; } // Primary Key
        public Guid StudentId { get; set; } // Foreign Key to Student Table
        public Guid PackageId { get; set; } // Foreign Key to Package Table

        public DateTime BillingDate { get; set; }
        public decimal TotalAmount { get; set; } // Before discounts
        public decimal Discount { get; set; }
        public decimal AmountDue { get; set; } // TotalAmount - Discount
        public decimal AmountPaid { get; set; }
        public DateTime? DueDate { get; set; }
        public string PaymentStatus { get; set; } // Example: "Paid", "Unpaid", "Partially Paid"
        public string PaymentMode { get; set; } // Example: "Cash", "Credit Card"
        public string InvoiceNumber { get; set; } // Unique invoice reference
       
    }

}
