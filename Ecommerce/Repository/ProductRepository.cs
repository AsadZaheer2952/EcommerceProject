using Ecommerce.Data;
using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly EcommStoreContext _context;
        public ProductRepository(EcommStoreContext context)
        {
            _context = context;

        }
        public async Task<Guid> AddProduct(Product product)
        {
            var Productrecord = new Product()
            {
                
                ProductDescription = product.ProductDescription,
                ProductName = product.ProductName,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = product.CreatedBy,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = product.UpdatedBy,
                DeletedAt = DateTime.UtcNow,
                DeletedBy = product.DeletedBy,

            };
          
            _context.Products.Add(Productrecord);
            var bridge = new ProductCategories()
            {
                ProductId = Productrecord.ProductId,
                CategoryId = product.CategoryId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = product.CreatedBy,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = product.UpdatedBy,
                DeletedAt = DateTime.UtcNow,
                DeletedBy = product.DeletedBy,
            };
            _context.ProductCategories.Add(bridge);
            await _context.SaveChangesAsync();
            return Productrecord.ProductId;

        }

     
        public async Task<List<Product>> GetAllProduct()
        {
            var records = await _context.Products.Select(x => new Product()
            {

                ProductDescription = x.ProductDescription,
                ProductName = x.ProductName,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = x.CreatedBy,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = x.UpdatedBy,
                DeletedAt = DateTime.UtcNow,
                DeletedBy = x.DeletedBy

            }).ToListAsync();
            return (records);
        }
        public async Task<Product> GetProductById(Guid ProductId)
        {
            var record = await _context.Products.Where(i => i.ProductId == ProductId).FirstOrDefaultAsync();
            return (record);
        }
        public async Task UpdateProduct(Guid ProductId, Product product)
        {
            var record = await _context.Products.FindAsync(ProductId);
            if (record != null)
            {

                record.ProductName = product.ProductName;
                record.ProductDescription = product.ProductDescription;
                record.UpdatedBy = product.UpdatedBy;

                await _context.SaveChangesAsync();


            }

        }
        public async Task DeleteProduct(Guid ProductId)
        {
            var delete = new Product() { ProductId = ProductId };


            _context.Products.Remove(delete);
            await _context.SaveChangesAsync();


        }

    }
}
   


