namespace DeliverySystem.Application.DTOs.CustomerOrders
{
    public class CreateCustomerRatingDto
    {
        public int OrderId { get; set; }
        public int? MerchantId { get; set; }
        public int? DeliveryAgentId { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
    }
}