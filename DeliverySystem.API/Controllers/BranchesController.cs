using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchesController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? search,
            [FromQuery] int? merchantId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var branches = await _branchService.GetAllBranchesAsync(search, merchantId, pageNumber, pageSize);
            return Ok(branches);
        }
        //=================================================== 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var branch = await _branchService.GetBranchByIdAsync(id);
            if (branch == null) return NotFound(new { message = "الفرع غير موجود" });
            return Ok(branch);
        }

        //=================================================== 
        [HttpGet("merchant/{merchantId}")]
        public async Task<IActionResult> GetByMerchant(int merchantId)
        {
            var branches = await _branchService.GetBranchesByMerchantAsync(merchantId);
            return Ok(branches);
        }

        //=================================================== 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBranchDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _branchService.CreateBranchAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { message = "تم إضافة الفرع بنجاح", id });
        }

        //=================================================== 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBranchDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _branchService.UpdateBranchAsync(id, dto);
            if (!result) return NotFound(new { message = "الفرع غير موجود للتعديل" });

            return Ok(new { message = "تم تعديل الفرع بنجاح" });
        }

        //=================================================== 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _branchService.DeleteBranchAsync(id);
            if (!result) return NotFound(new { message = "الفرع غير موجود للحذف" });

            return Ok(new { message = "تم حذف الفرع بنجاح" });
        }
    }
}