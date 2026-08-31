namespace DeliverySystem.Application.DTOs.CustomerOrders
{
    public class CreateCustomerOrderDto
    {
        public int BranchId { get; set; }
        public int AddressId { get; set; }
        public int? CouponId { get; set; }
        public List<CreateCustomerOrderItemDto> Items { get; set; } = new();
    }

    public class CreateCustomerOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}