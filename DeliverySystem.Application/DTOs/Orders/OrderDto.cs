namespace DeliverySystem.Application.DTOs.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public int BranchId { get; set; }
        public int? DeliveryAgentId { get; set; }
        public int AddressId { get; set; }
        public int? CouponId { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
    }
}