using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        //=================================================== 
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? search,
            [FromQuery] int? categoryId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var products = await _productService.GetAllProductsAsync(search, categoryId, pageNumber, pageSize);
            return Ok(products);
        }

        //=================================================== 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound(new { message = "المنتج غير موجود" });

            return Ok(product);
        }


        //=================================================== 

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _productService.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { message = "تم إضافة المنتج بنجاح", id });
        }


        //=================================================== 

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.UpdateProductAsync(id, dto);
            if (!result)
                return NotFound(new { message = "المنتج غير موجود للتعديل" });

            return Ok(new { message = "تم تعديل المنتج بنجاح" });
        }

        //=================================================== 

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
                return NotFound(new { message = "المنتج غير موجود للحذف" });

            return Ok(new { message = "تم حذف المنتج بنجاح" });
        }
    }
}