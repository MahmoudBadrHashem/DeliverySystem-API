namespace DeliverySystem.Domain.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        // foreign key of ApplicationUser 
        public string UserId { get; set; } = null!;
        public int? MerchantId { get; set; }
        public int? DeliveryAgentId { get; set; }

        public int Score { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Order Order { get; set; } = null!;
        public User Customer { get; set; } = null!;
        public Merchant? Merchant { get; set; }
        public DeliveryAgent? DeliveryAgent { get; set; }
    }
}