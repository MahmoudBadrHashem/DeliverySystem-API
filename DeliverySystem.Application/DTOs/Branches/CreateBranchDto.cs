namespace DeliverySystem.Application.DTOs.Branches
{
    public class CreateBranchDto
    {
        public int MerchantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}