namespace DeliverySystem.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        // foreign key of ApplicationUser 
        public string UserId { get; set; } = default!;
    }
}