using DeliverySystem.Domain.Enums;

namespace DeliverySystem.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public string? TransactionId { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime? PaymentDate { get; set; }

        // Navigation Property
        public Order Order { get; set; } = null!;
    }
}