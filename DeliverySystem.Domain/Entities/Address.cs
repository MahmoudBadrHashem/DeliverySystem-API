namespace DeliverySystem.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        // foreign key of ApplicationUser 
        public string UserId { get; set; } = default!;       
        public string StreetName { get; set; } = string.Empty;
        public string BuildingNumber { get; set; } = string.Empty;
        public string? FloorNumber { get; set; }
        public string? ApartmentNumber { get; set; }
        public string? AdditionalDirections { get; set; }
        public string Label { get; set; } = "Home"; // e.g., Home, Work, Other
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}