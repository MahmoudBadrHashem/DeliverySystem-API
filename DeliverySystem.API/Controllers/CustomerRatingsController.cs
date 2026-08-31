using DeliverySystem.Application.DTOs.CustomerOrders;
using DeliverySystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.API.Controllers
{
    [Route("api/customer/ratings")]
    [Authorize]
    public class CustomerRatingsController : BaseApiController
    {
        private readonly ICustomerRatingService _customerRatingService;

        public CustomerRatingsController(ICustomerRatingService customerRatingService)
        {
            _customerRatingService = customerRatingService;
        }

        [HttpPost]
        public async Task<IActionResult> RateOrder([FromBody] CreateCustomerRatingDto dto)
        {
            try
            {
                var id = await _customerRatingService.RateOrderAsync(CurrentUserId, dto);
                return Ok(new { message = "تم إضافة التقييم بنجاح", id });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}