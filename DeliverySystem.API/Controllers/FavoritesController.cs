using DeliverySystem.Application.DTOs.Favorites;
using DeliverySystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetByCustomer(int customerId)
        {
            var favorites = await _favoriteService.GetCustomerFavoritesAsync(customerId);
            return Ok(favorites);
        }

        //=================================================== 

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateFavoriteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _favoriteService.AddToFavoritesAsync(dto);
            if (!result)
                return BadRequest(new { message = "المنتج موجود بالفعل في المفضلة أو البيانات غير صالحة" });

            return Ok(new { message = "تمت الإضافة إلى المفضلة بنجاح" });
        }

        //=================================================== 

        [HttpDelete("customer/{customerId}/product/{productId}")]
        public async Task<IActionResult> Remove(int customerId, int productId)
        {
            var result = await _favoriteService.RemoveFromFavoritesAsync(customerId, productId);
            if (!result)
                return NotFound(new { message = "لم يتم العثور على المنتج في المفضلة" });

            return Ok(new { message = "تمت الإزالة من المفضلة بنجاح" });
        }
    }
}