using DeliverySystem.Application.DTOs.CustomerOrders;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;
using DeliverySystem.Domain.Enums;

namespace DeliverySystem.Application.Services
{
    public class CustomerRatingService : ICustomerRatingService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerRatingService(
            IOrderRepository orderRepository,
            IRatingRepository ratingRepository,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _ratingRepository = ratingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> RateOrderAsync(string userId, CreateCustomerRatingDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(dto.OrderId);

            if (order == null || order.UserId != userId)
                throw new InvalidOperationException("الأوردر غير موجود");

            if (order.Status != OrderStatus.Delivered)
                throw new InvalidOperationException("لا يمكن تقييم أوردر لم يتم تسليمه بعد");

            if (dto.Score < 1 || dto.Score > 5)
                throw new InvalidOperationException("التقييم يجب أن يكون بين 1 و 5");

            var rating = new Rating
            {
                OrderId = dto.OrderId,
                UserId = userId,
                MerchantId = dto.MerchantId,
                DeliveryAgentId = dto.DeliveryAgentId,
                Score = dto.Score,
                Comment = dto.Comment,
                CreatedDate = DateTime.UtcNow
            };

            await _ratingRepository.AddAsync(rating);
            await _unitOfWork.SaveChangesAsync();
            return rating.Id;
        }
    }
}