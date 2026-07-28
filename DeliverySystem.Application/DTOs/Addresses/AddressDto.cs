namespace DeliverySystem.Application.DTOs.Addresses
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string StreetName { get; set; } = string.Empty;
        public string BuildingNumber { get; set; } = string.Empty;
        public string? FloorNumber { get; set; }
        public string? ApartmentNumber { get; set; }
        public string? AdditionalDirections { get; set; }
        public string Label { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
