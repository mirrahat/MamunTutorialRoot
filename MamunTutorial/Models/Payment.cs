namespace MamunTutorial.Models
{
    public class Payment
    {
        public Guid PaymentId { get; set; } // Primary Key
        public Guid BillingId { get; set; } // Foreign Key to Billing Table
        public Guid StudentId { get; set; } // Foreign Key to Student Table

        public decimal TotalAmount { get; set; } // Total amount to be paid
        public DateTime PaymentDate { get; set; } // Date of payment
        public decimal CreditPayment { get; set; } // Payment made through credit
        public decimal CashPayment { get; set; } // Payment made through cash
        public decimal Due { get; set; } // Remaining due after payment

        public string PaymentMethod { get; set; } // E.g., Credit Card, Cash, Bank Transfer
        public string PaymentStatus { get; set; } // E.g., "Completed", "Pending", "Failed"
        public string TransactionId { get; set; } // Unique transaction identifier for the payment
        public string PaymentReference { get; set; } // Any reference or notes related to the payment

        public DateTime CreatedDate { get; set; } // Date when the payment was created
        public DateTime? ModifiedDate { get; set; } // Date when the payment was last modified
    }
}
