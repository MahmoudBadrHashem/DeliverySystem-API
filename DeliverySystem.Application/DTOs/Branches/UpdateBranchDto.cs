namespace DeliverySystem.Application.DTOs.Branches
{
    public class UpdateBranchDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsActive { get; set; }
        public int MerchantId { get; set; }

    }
}