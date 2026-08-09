using DeliverySystem.Application.DTOs.Orders;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;
using DeliverySystem.Domain.Enums;

namespace DeliverySystem.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                CustomerId = o.UserId,   
                BranchId = o.BranchId,
                DeliveryAgentId = o.DeliveryAgentId,
                AddressId = o.AddressId,
                CouponId = o.CouponId,
                Status = o.Status.ToString(),
                TotalAmount = o.TotalAmount,
                DiscountAmount = o.DiscountAmount,
                CreatedDate = o.CreatedDate,
                DeliveredDate = o.DeliveredDate
            });
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var o = await _orderRepository.GetByIdAsync(id);
            if (o == null) return null;

            return new OrderDto
            {
                Id = o.Id,
                CustomerId = o.UserId,
                BranchId = o.BranchId,
                DeliveryAgentId = o.DeliveryAgentId,
                AddressId = o.AddressId,
                CouponId = o.CouponId,
                Status = o.Status.ToString(),
                TotalAmount = o.TotalAmount,
                DiscountAmount = o.DiscountAmount,
                CreatedDate = o.CreatedDate,
                DeliveredDate = o.DeliveredDate
            };
        }

        public async Task<int> CreateOrderAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                UserId = dto.CustomerId,
                BranchId = dto.BranchId,
                AddressId = dto.AddressId,
                CouponId = dto.CouponId,
                TotalAmount = dto.TotalAmount,
                DiscountAmount = dto.DiscountAmount,
                Status = OrderStatus.Pending,
                CreatedDate = DateTime.UtcNow
            };

            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();   
            return order.Id;
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, UpdateOrderStatusDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return false;

            order.Status = (OrderStatus)dto.Status;
            if (dto.DeliveryAgentId.HasValue)
                order.DeliveryAgentId = dto.DeliveryAgentId;

            if (order.Status == OrderStatus.Delivered)
                order.DeliveredDate = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return false;

            await _orderRepository.DeleteAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}