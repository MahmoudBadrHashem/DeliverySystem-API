using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
        {
            var orders = await _orderRepository.GetAllAsync(cancellationToken);
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

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            var orders = await _orderRepository.GetOrdersByCustomerAsync(userId, cancellationToken);
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

        public async Task<OrderDto?> GetOrderByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var o = await _orderRepository.GetByIdAsync(id, cancellationToken);
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

        public async Task<int> CreateOrderAsync(CreateOrderDto dto, CancellationToken cancellationToken = default)
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

            await _orderRepository.AddAsync(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);   
            return order.Id;
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, UpdateOrderStatusDto dto, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
            if (order == null) return false;

            order.Status = (OrderStatus)dto.Status;
            if (dto.DeliveryAgentId.HasValue)
                order.DeliveryAgentId = dto.DeliveryAgentId;

            if (order.Status == OrderStatus.Delivered)
                order.DeliveredDate = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
            if (order == null) return false;

            await _orderRepository.DeleteAsync(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}