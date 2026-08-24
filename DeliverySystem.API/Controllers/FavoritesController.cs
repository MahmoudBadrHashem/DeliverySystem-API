using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Favorites;
using DeliverySystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("my-favorites")]
        public async Task<IActionResult> GetMyFavorites()
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                ?? throw new UnauthorizedAccessException("Invalid token ");
            if (!int.TryParse(userId, out int customerId))
                return BadRequest(new { message = "معرف المستخدم غير صالح" });

            var favorites = await _favoriteService.GetCustomerFavoritesAsync(customerId);
            return Ok(favorites);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateFavoriteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                ?? throw new UnauthorizedAccessException("Invalid token ");
            if (!int.TryParse(userId, out int customerId))
                return BadRequest(new { message = "معرف المستخدم غير صالح" });

            dto.CustomerId = customerId;

            var result = await _favoriteService.AddToFavoritesAsync(dto);
            if (!result)
                return BadRequest(new { message = "المنتج موجود بالفعل في المفضلة أو البيانات غير صالحة" });

            return Ok(new { message = "تمت الإضافة إلى المفضلة بنجاح" });
        }

        [Authorize]
        [HttpDelete("product/{productId}")]
        public async Task<IActionResult> Remove(int productId)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                ?? throw new UnauthorizedAccessException("Invalid token ");
            if (!int.TryParse(userId, out int customerId))
                return BadRequest(new { message = "معرف المستخدم غير صالح" });

            var result = await _favoriteService.RemoveFromFavoritesAsync(customerId, productId);
            if (!result)
                return NotFound(new { message = "لم يتم العثور على المنتج في المفضلة" });

            return Ok(new { message = "تمت الإزالة من المفضلة بنجاح" });
        }
    }
}