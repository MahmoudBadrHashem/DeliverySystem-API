namespace DeliverySystem.Domain.Entities
{
    public class DeliveryAgent
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;
        public string? VehicleType { get; set; }
        public bool IsAvailable { get; set; }
       public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}