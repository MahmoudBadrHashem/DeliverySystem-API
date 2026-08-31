namespace DeliverySystem.Application.DTOs.CustomerOrders
{
    public class CustomerOrderDto
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
    }
}