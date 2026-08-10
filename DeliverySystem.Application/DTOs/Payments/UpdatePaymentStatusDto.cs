namespace DeliverySystem.Application.DTOs.Payments
{
    public class UpdatePaymentStatusDto
    {
        public int Status { get; set; }
        public string? TransactionId { get; set; }
    }
}