namespace DeliverySystem.Application.DTOs.Payments
{
    public class CreatePaymentDto
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public int Method { get; set; }  
    }
}