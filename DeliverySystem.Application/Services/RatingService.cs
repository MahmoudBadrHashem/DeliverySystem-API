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

        public async Task<IEnumerable<RatingDto>> GetAllRatingsAsync()
        {
            var ratings = await _ratingRepository.GetAllAsync();
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

        public async Task<RatingDto?> GetRatingByIdAsync(int id)
        {
            var r = await _ratingRepository.GetByIdAsync(id);
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

        public async Task<int> CreateRatingAsync(CreateRatingDto dto)
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

            await _ratingRepository.AddAsync(rating);
            await _unitOfWork.SaveChangesAsync();
            return rating.Id;
        }

        public async Task<bool> DeleteRatingAsync(int id)
        {
            var rating = await _ratingRepository.GetByIdAsync(id);
            if (rating == null) return false;

            await _ratingRepository.DeleteAsync(rating);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}