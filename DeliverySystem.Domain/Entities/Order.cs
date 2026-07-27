using DeliverySystem.Domain.Enums;
using System.Net;

namespace DeliverySystem.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public int BranchId { get; set; }
        public int? DeliveryAgentId { get; set; }
        public int AddressId { get; set; }
        public int? CouponId { get; set; }
        // foreign key of ApplicationUser 
        public string UserId { get; set; }=default!;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeliveredDate { get; set; }

        // Navigation Properties
        public User Customer { get; set; } = null!;
        public Branch Branch { get; set; } = null!;
        public DeliveryAgent? DeliveryAgent { get; set; }
        public Address Address { get; set; } = null!;
        public Coupon? Coupon { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Payment? Payment { get; set; }
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}