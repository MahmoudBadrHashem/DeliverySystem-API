using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Ratings;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RatingService(IRatingRepository ratingRepository, IUnitOfWork unitOfWork)
        {
            _ratingRepository = ratingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RatingDto>> GetAllRatingsAsync(CancellationToken cancellationToken = default)
        {
            var ratings = await _ratingRepository.GetAllAsync(cancellationToken);
            return ratings.Select(r => new RatingDto
            {
                Id = r.Id,
                OrderId = r.OrderId,
                CustomerId = r.UserId,   
                MerchantId = r.MerchantId,
                DeliveryAgentId = r.DeliveryAgentId,
                Score = r.Score,
                Comment = r.Comment,
                CreatedDate = r.CreatedDate
            });
        }

        public async Task<RatingDto?> GetRatingByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var r = await _ratingRepository.GetByIdAsync(id, cancellationToken);
            if (r == null) return null;

            return new RatingDto
            {
                Id = r.Id,
                OrderId = r.OrderId,
                CustomerId = r.UserId,
                MerchantId = r.MerchantId,
                DeliveryAgentId = r.DeliveryAgentId,
                Score = r.Score,
                Comment = r.Comment,
                CreatedDate = r.CreatedDate
            };
        }

        public async Task<IEnumerable<RatingDto>> GetRatingsByOrderIdAsync(int orderId, CancellationToken cancellationToken = default)
        {
            var ratings = await _ratingRepository.GetAllAsync(cancellationToken);
            var orderRatings = ratings.Where(r => r.OrderId == orderId);
            return orderRatings.Select(r => new RatingDto
            {
                Id = r.Id,
                OrderId = r.OrderId,
                CustomerId = r.UserId,
                MerchantId = r.MerchantId,
                DeliveryAgentId = r.DeliveryAgentId,
                Score = r.Score,
                Comment = r.Comment,
                CreatedDate = r.CreatedDate
            });
        }

        public async Task<int> CreateRatingAsync(CreateRatingDto dto, CancellationToken cancellationToken = default)
        {
            var rating = new Rating
            {
                OrderId = dto.OrderId,
                UserId = dto.CustomerId,  
                MerchantId = dto.MerchantId,
                DeliveryAgentId = dto.DeliveryAgentId,
                Score = dto.Score,
                Comment = dto.Comment,
                CreatedDate = DateTime.UtcNow
            };

            await _ratingRepository.AddAsync(rating, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return rating.Id;
        }

        public async Task<bool> DeleteRatingAsync(int id, CancellationToken cancellationToken = default)
        {
            var rating = await _ratingRepository.GetByIdAsync(id, cancellationToken);
            if (rating == null) return false;

            await _ratingRepository.DeleteAsync(rating, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}