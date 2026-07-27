namespace DeliverySystem.Application.DTOs.Favorites
{
    public class FavoriteDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; } = string.Empty;
    }
}



