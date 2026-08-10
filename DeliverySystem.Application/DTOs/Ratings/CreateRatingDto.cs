namespace DeliverySystem.Application.DTOs.Ratings
{
    public class CreateRatingDto
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public int? MerchantId { get; set; }
        public int? DeliveryAgentId { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
    }
}