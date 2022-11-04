using Ecommerce.Model;
using Ecommerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
        [HttpPost("UploadExcel")]
        public async Task<IActionResult> UploadExcle([FromForm] Upload upload)
        { 
           
                var result = await _product.UploadExcelFile(upload);
            return Ok();


    }
        [HttpGet("Genrate ExcleFile")]
        public async Task<IActionResult> ExportFile()
        {
            var res = await _product.ExportExcelFile();
            return File(
                res,
                "application/vnd.ms-excel",
                "products.xlsx"

                );
        }
        [HttpGet("Genrate PdfFile")]
        public async Task<IActionResult> Export()
        {
            var result= await _product.ExportPdfFile();
            return File(
                result,
                "application/vnd.ms-pdf",
                "products.pdf"

                );
        }
        [HttpGet("thread")]
        public async Task<IActionResult> ThreadingTest()
        {
                int i = 0;
                Thread t = new Thread(() =>
                {
                    Console.WriteLine("Start Thread.");

                   for (i = 0; i < 10; i++)
                        Console.WriteLine(i + "Thread Running!");

                    Console.WriteLine("End Thread.");
                });


            Thread t1 = new Thread(() =>
                           {
                               Console.WriteLine("Start Thread.");

                               for (i = 0; i < 10; i++)
                                   Console.WriteLine(i);

                               Console.WriteLine("End Thread.");
                           });
            Thread t2= new Thread(() =>
            {
                Console.WriteLine("Start Thread.");

                Parallel.For(0, 10, i =>
                {
                    Console.WriteLine(i);

                    
                });
                });
            t1.Start();
            Thread.Sleep(10000);
            Console.WriteLine("thread sleep");
            t2.Start();
            t2.Join();
            Thread.Sleep(3000);
            Console.WriteLine("Thread finished. Count: " + i);
            return Ok(i);
            
        }
      


    }

}
