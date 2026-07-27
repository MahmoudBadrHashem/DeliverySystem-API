using DeliverySystem.Application.DTOs.Categories;
using DeliverySystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //=================================================== 
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? search,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var categories = await _categoryService.GetAllCategoriesAsync(search, pageNumber, pageSize);
            return Ok(categories);
        }

        //=================================================== 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound(new { message = "التصنيف غير موجود" });
            return Ok(category);
        }

        //=================================================== 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _categoryService.CreateCategoryAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { message = "تم إضافة التصنيف بنجاح", id });
        }

        //=================================================== 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _categoryService.UpdateCategoryAsync(id, dto);
            if (!result) return NotFound(new { message = "التصنيف غير موجود للتعديل" });

            return Ok(new { message = "تم تعديل التصنيف بنجاح" });
        }

        //=================================================== 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result) return NotFound(new { message = "التصنيف غير موجود للحذف" });

            return Ok(new { message = "تم حذف التصنيف بنجاح" });
        }
    }
}