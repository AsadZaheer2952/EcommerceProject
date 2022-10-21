using Ecommerce.Model;
using Ecommerce.Paging;
using Ecommerce.Query;

namespace Ecommerce.Repository
{
    public interface ICategory
    {
        Task<Guid> AddCategoryAsync(Category category);
        Task<List<Category>> GetAllCategory();
        Task<Category> GetCategoryById(Guid CategoryId);
        Task<dynamic> CategoryPagination(Pagination pagination);
        Task UpdateCategory(Guid CategoryId, Category category);
        Task DeleteCategory(Guid CategoryId);
        Task<object> CategoryFiltering(FilterQuery filter);
    }
}
