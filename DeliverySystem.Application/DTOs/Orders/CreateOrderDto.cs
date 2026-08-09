namespace DeliverySystem.Application.DTOs.Orders
{
    public class CreateOrderDto
    {
        public string CustomerId { get; set; } = string.Empty;
        public int BranchId { get; set; }
        public int AddressId { get; set; }
        public int? CouponId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}