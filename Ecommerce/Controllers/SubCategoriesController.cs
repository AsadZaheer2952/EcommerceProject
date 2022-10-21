using Ecommerce.Model;
using Ecommerce.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {

        private readonly ISubCategories _subCategories;

        public SubCategoriesController(ISubCategories subCategories)
        {
            _subCategories = subCategories;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllsub()
        {
            var gets = await _subCategories.GetAllSubCategories();
            return Ok(gets);
        }
    }

}