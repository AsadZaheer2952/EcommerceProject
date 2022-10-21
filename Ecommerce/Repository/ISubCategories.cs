using Ecommerce.Model;

namespace Ecommerce.Repository
{
    public interface ISubCategories
    {

     /*   Task<ProductCategories> AddProductCategories(Guid ProductId, Guid CategoryId);*/
        Task<List<ProductCategories>> GetAllSubCategories();
    }
}
