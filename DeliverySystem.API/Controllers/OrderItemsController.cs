using DeliverySystem.Application.DTOs.OrderItems;
using DeliverySystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemsController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _orderItemService.GetAllOrderItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _orderItemService.GetOrderItemByIdAsync(id);
            if (item == null) return NotFound(new { message = "عنصر الأوردر غير موجود" });
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderItemDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = await _orderItemService.CreateOrderItemAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { message = "تم إضافة العنصر بنجاح", id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _orderItemService.DeleteOrderItemAsync(id);
            if (!result) return NotFound(new { message = "عنصر الأوردر غير موجود" });
            return Ok(new { message = "تم الحذف بنجاح" });
        }
    }
}