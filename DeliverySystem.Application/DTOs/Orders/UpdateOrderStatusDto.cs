namespace DeliverySystem.Application.DTOs.Orders
{
    public class UpdateOrderStatusDto
    {
        public int Status { get; set; }   
        public int? DeliveryAgentId { get; set; }
    }
}