using DeliverySystem.Application.DTOs.CustomerOrders;
using DeliverySystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.API.Controllers
{
    [Route("api/customer/orders")]
    [Authorize]   // لازم العميل يكون مسجل دخول
    public class CustomerOrdersController : BaseApiController
    {
        private readonly ICustomerOrderService _customerOrderService;

        public CustomerOrdersController(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyOrders()
        {
            var orders = await _customerOrderService.GetMyOrdersAsync(CurrentUserId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMyOrderById(int id)
        {
            var order = await _customerOrderService.GetMyOrderByIdAsync(CurrentUserId, id);
            if (order == null) return NotFound(new { message = "الأوردر غير موجود" });
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerOrderDto dto)
        {
            try
            {
                var id = await _customerOrderService.CreateOrderAsync(CurrentUserId, dto);
                return CreatedAtAction(nameof(GetMyOrderById), new { id }, new { message = "تم إنشاء الأوردر بنجاح", id });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                var result = await _customerOrderService.CancelOrderAsync(CurrentUserId, id);
                if (!result) return NotFound(new { message = "الأوردر غير موجود" });
                return Ok(new { message = "تم إلغاء الأوردر" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}