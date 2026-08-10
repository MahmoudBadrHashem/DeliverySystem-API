using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Addresses;
using DeliverySystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var addresses = await _addressService.GetAllAddressesAsync();
            return Ok(addresses);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var addresses = await _addressService.GetAddressesByUserIdAsync(userId);
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null)
                return NotFound(new { message = "العنوان غير موجود" });

            return Ok(address);
        }

        [HttpPost("user/{userId}")]
        public async Task<IActionResult> Create(string userId, [FromBody] CreateAddressDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _addressService.CreateAddressAsync(userId, dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { message = "تم إضافة العنوان بنجاح", id });
        }

        [HttpPut("{id}/user/{userId}")]
        public async Task<IActionResult> Update(int id, string userId, [FromBody] UpdateAddressDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _addressService.UpdateAddressAsync(id, userId, dto);
            if (!result)
                return NotFound(new { message = "العنوان غير موجود أو لا ينتمي لهذا المستخدم" });

            return Ok(new { message = "تم تعديل العنوان بنجاح" });
        }

        [HttpDelete("{id}/user/{userId}")]
        public async Task<IActionResult> Delete(int id, string userId)
        {
            var result = await _addressService.DeleteAddressAsync(id, userId);
            if (!result)
                return NotFound(new { message = "العنوان غير موجود أو لا ينتمي لهذا المستخدم" });

            return Ok(new { message = "تم حذف العنوان بنجاح" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGlobal(int id)
        {
            var result = await _addressService.DeleteAddressAsync(id);
            if (!result)
                return NotFound(new { message = "العنوان غير موجود" });

            return Ok(new { message = "تم حذف العنوان بنجاح" });
        }
    }
}
