using DeliverySystem.Application.DTOs.Merchants;
using DeliverySystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private readonly IMerchantService _merchantService;

        public MerchantsController(IMerchantService merchantService)
        {
            _merchantService = merchantService;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? search,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var merchants = await _merchantService.GetAllMerchantsAsync(search, pageNumber, pageSize);
            return Ok(merchants);
        }

        //=================================================== 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var merchant = await _merchantService.GetMerchantByIdAsync(id);
            if (merchant == null)
                return NotFound(new { message = "التاجر غير موجود" });

            return Ok(merchant);
        }

        //=================================================== 
       
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMerchantDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _merchantService.CreateMerchantAsync(createDto);
            return Ok(new { message = "تم إضافة التاجر بنجاح" });
        }

        //=================================================== 
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMerchantDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _merchantService.UpdateMerchantAsync(id, updateDto);
            if (!result)
                return NotFound(new { message = "التاجر غير موجود للتعديل" });

            return Ok(new { message = "تم تعديل بيانات التاجر بنجاح" });
        }

        //=================================================== 
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var merchant = await _merchantService.GetMerchantByIdAsync(id);
            if (merchant == null)
                return NotFound(new { message = "التاجر غير موجود للحذف" });

            await _merchantService.DeleteMerchantAsync(id);
            return Ok(new { message = "تم حذف التاجر بنجاح" });
        }
    }
}