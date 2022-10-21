using Ecommerce.Model;

namespace Ecommerce.Repository
{
    public interface IProduct
    {
        Task<Guid> AddProduct(Product product);
        Task<List<Product>> GetAllProduct();
        Task<Product> GetProductById(Guid ProductId);
        Task UpdateProduct(Guid ProductId, Product product);
        Task DeleteProduct(Guid ProductId);
    }
}
