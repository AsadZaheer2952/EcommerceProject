using Ecommerce.Model;
using Ecommerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase

    {
     private readonly IProduct _product;

    public ProductController(IProduct product)
        {
            _product = product;
        }
        [HttpPost]
        public async Task<IActionResult> Addproducts(Product product)
        {
            var add = await _product.AddProduct(product);
            return Ok(add);
        }
        [HttpGet("AllProduct")]
        public async Task<IActionResult> GetAll()
        {
            var get = await _product.GetAllProduct();
            return Ok(get);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid ProductId)
        {
            var pro = await _product.GetProductById(ProductId);
            return Ok(pro);
        }
        [HttpPut("{ProductId}")]
        public async Task<IActionResult> Update([FromBody] Product product, [FromRoute] Guid ProductId)
        {
            await _product.UpdateProduct(ProductId, product);
            return Ok();
        }
        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid ProductId)
        {
            await _product.DeleteProduct(ProductId);
            return Ok("Successfully deleted");
        }
    }
}
