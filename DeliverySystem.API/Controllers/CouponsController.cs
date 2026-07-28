using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Coupons;
using DeliverySystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponsController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var coupons = await _couponService.GetAllCouponsAsync();
            return Ok(coupons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var coupon = await _couponService.GetCouponByIdAsync(id);
            if (coupon == null)
                return NotFound(new { message = "كود الخصم غير موجود" });

            return Ok(coupon);
        }

        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var coupon = await _couponService.GetCouponByCodeAsync(code);
            if (coupon == null)
                return NotFound(new { message = "كود الخصم غير موجود" });

            return Ok(coupon);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCouponDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _couponService.CreateCouponAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { message = "تم إضافة كود الخصم بنجاح", id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCouponDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _couponService.UpdateCouponAsync(id, dto);
            if (!result)
                return NotFound(new { message = "كود الخصم غير موجود للتعديل" });

            return Ok(new { message = "تم تعديل كود الخصم بنجاح" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _couponService.DeleteCouponAsync(id);
            if (!result)
                return NotFound(new { message = "كود الخصم غير موجود للحذف" });

            return Ok(new { message = "تم حذف كود الخصم بنجاح" });
        }

        [HttpGet("validate/{code}")]
        public async Task<IActionResult> Validate(string code)
        {
            var isValid = await _couponService.ValidateCouponAsync(code);
            if (!isValid)
                return Ok(new { isValid = false, message = "كود الخصم غير صالح أو منتهي الصلاحية" });

            var coupon = await _couponService.GetCouponByCodeAsync(code);
            return Ok(new { isValid = true, message = "كود الخصم صالح للاستخدام", coupon });
        }
    }
}
