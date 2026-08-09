using DeliverySystem.Application.DTOs.OrderItems;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderItemService(IOrderItemRepository orderItemRepository, IUnitOfWork unitOfWork)
        {
            _orderItemRepository = orderItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderItemDto>> GetAllOrderItemsAsync()
        {
            var items = await _orderItemRepository.GetAllAsync();
            return items.Select(i => new OrderItemDto
            {
                Id = i.Id,
                OrderId = i.OrderId,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Subtotal = i.Subtotal
            });
        }

        public async Task<OrderItemDto?> GetOrderItemByIdAsync(int id)
        {
            var i = await _orderItemRepository.GetByIdAsync(id);
            if (i == null) return null;

            return new OrderItemDto
            {
                Id = i.Id,
                OrderId = i.OrderId,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Subtotal = i.Subtotal
            };
        }

        public async Task<int> CreateOrderItemAsync(CreateOrderItemDto dto)
        {
            var item = new OrderItem
            {
                OrderId = dto.OrderId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                Subtotal = dto.Quantity * dto.UnitPrice
            };

            await _orderItemRepository.AddAsync(item);
            await _unitOfWork.SaveChangesAsync();
            return item.Id;
        }

        public async Task<bool> DeleteOrderItemAsync(int id)
        {
            var item = await _orderItemRepository.GetByIdAsync(id);
            if (item == null) return false;

            await _orderItemRepository.DeleteAsync(item);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}