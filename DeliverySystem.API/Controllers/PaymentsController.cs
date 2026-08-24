using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Payments;
using DeliverySystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null) return NotFound(new { message = "الدفعة غير موجودة" });
            return Ok(payment);
        }

        [Authorize]
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetByOrderId(int orderId)
        {
            var payment = await _paymentService.GetPaymentByOrderIdAsync(orderId);
            if (payment == null) return NotFound(new { message = "الدفعة غير موجودة لهذا الأوردر" });
            return Ok(payment);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = await _paymentService.CreatePaymentAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { message = "تم إنشاء الدفعة بنجاح", id });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdatePaymentStatusDto dto)
        {
            var result = await _paymentService.UpdatePaymentStatusAsync(id, dto);
            if (!result) return NotFound(new { message = "الدفعة غير موجودة" });
            return Ok(new { message = "تم تحديث حالة الدفعة" });
        }
    }
}