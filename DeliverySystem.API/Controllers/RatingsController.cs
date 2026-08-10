using DeliverySystem.Application.DTOs.Ratings;
using DeliverySystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ratings = await _ratingService.GetAllRatingsAsync();
            return Ok(ratings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rating = await _ratingService.GetRatingByIdAsync(id);
            if (rating == null) return NotFound(new { message = "التقييم غير موجود" });
            return Ok(rating);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRatingDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = await _ratingService.CreateRatingAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { message = "تم إضافة التقييم بنجاح", id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ratingService.DeleteRatingAsync(id);
            if (!result) return NotFound(new { message = "التقييم غير موجود" });
            return Ok(new { message = "تم الحذف بنجاح" });
        }
    }
}