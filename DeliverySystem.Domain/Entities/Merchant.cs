namespace DeliverySystem.Domain.Entities
{
    public class Merchant
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserId { get; set; } = null!;
        public string BusinessName { get; set; } = null!;
        public string? CommercialRegister { get; set; }
        public string? TaxNumber { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public ICollection<Branch> Branches { get; set; } = new List<Branch>();

    }
}