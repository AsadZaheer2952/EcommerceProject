using Ecommerce.Model;
using Ecommerce.Paging;
using Ecommerce.Query;
using Ecommerce.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _Category;
        public CategoryController(ICategory category)
        {
            _Category = category;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Category category)
        {


            var add = await _Category.AddCategoryAsync(category);
            return Ok(add);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllRecord()
        {
            var record = await _Category.GetAllCategory();
            return Ok(record);
        }
        [HttpGet("{CategoryId}")]
        public async Task<IActionResult> GetCategory([FromRoute] Guid CategoryId)
        {
            var catog = await _Category.GetCategoryById(CategoryId);
            return Ok(catog);
        }
        [HttpGet("/pagination")]
        public async Task<IActionResult> Pagination([FromQuery] Pagination pagination)
        {
            var response = await _Category.CategoryPagination(pagination);
            return Ok(response);
        }
        [HttpPut("{CategoryId}")]
        public async Task<IActionResult> Update([FromBody] Category category, [FromRoute] Guid CategoryId)
        {
            await _Category.UpdateCategory(CategoryId, category);
            return Ok();
        }
        [HttpDelete("{CategoryId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid CategoryId)
        {
            await _Category.DeleteCategory(CategoryId);
            return Ok();
        }
        [HttpGet("/filter")]
        public async Task<IActionResult> CategoryFilter([FromQuery] FilterQuery filter)
        {
            var response = await _Category.CategoryFiltering(filter);
            return Ok(response);
        }
    }
}