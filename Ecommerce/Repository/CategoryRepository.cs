using Ecommerce.Data;
using Ecommerce.Model;
using Ecommerce.Paging;
using Ecommerce.Query;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using static System.Reflection.Metadata.BlobBuilder;

namespace Ecommerce.Repository
{
    public class CategoryRepository : ICategory
    {
        private readonly EcommStoreContext _context;
        public CategoryRepository(EcommStoreContext context)
        {
            _context = context;

        }
        public async Task<Guid> AddCategoryAsync(Category category)
        {
            var Categoryrecord = new Category()
            {

                Category_Name = category.Category_Name,
                Category_Description = category.Category_Description,
                ParentId = category.ParentId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = category.CreatedBy,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = category.UpdatedBy,
                DeletedAt = DateTime.UtcNow,
                DeletedBy = category.DeletedBy,

            };
            _context.Categories.Add(Categoryrecord);
            await _context.SaveChangesAsync();
            return Categoryrecord.CategoryId;
        }
        public async Task<List<Category>> GetAllCategory()
        {
            var record = await _context.Categories.Select(x => new Category()
            {

                CategoryId = x.CategoryId,
                Category_Name = x.Category_Name,
                Category_Description = x.Category_Description,
                ParentId = x.ParentId,
                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy,
                UpdatedAt = x.UpdatedAt,
                UpdatedBy = x.UpdatedBy,
                DeletedAt = x.DeletedAt,
                DeletedBy = x.DeletedBy,

            }).ToListAsync();
            return (record);
        }
        public async Task<Category> GetCategoryById(Guid CategoryId)
        {
            var records = await _context.Categories.Where(i => i.CategoryId == CategoryId).FirstOrDefaultAsync();
            return records;
        }
        public async Task<dynamic> CategoryPagination(Pagination pagination)
        {
            var paging = await (from s in _context.Categories

                                select new
                                {
                                    s.CategoryId,
                                    s.Category_Name,
                                    s.Category_Description,
                                    s.ParentId,
                                    s.CreatedAt,
                                    s.CreatedBy,
                                    s.UpdatedAt,
                                    s.UpdatedBy,
                                    s.DeletedAt,
                                    s.DeletedBy,



                                }).OrderBy(s => s.CategoryId).Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync();

            return paging;
        }


        public async Task UpdateCategory(Guid CategoryId, Category category)
        {
            var record = await _context.Categories.FindAsync(CategoryId);
            if (record != null)
            {

                record.Category_Name = category.Category_Name;
                record.Category_Description = category.Category_Description;

                await _context.SaveChangesAsync();


            }

        }
        public async Task DeleteCategory(Guid CategoryId)
        {
            var delete = new Category() { CategoryId = CategoryId };


            _context.Categories.Remove(delete);
            await _context.SaveChangesAsync();


        }
        public async Task<object> CategoryFiltering(FilterQuery filter)

        {
            var records = await (from s in _context.Categories
                                 where s.Category_Name == filter.Category_Name
                                 select new
                                 {
                                     s.CategoryId,
                                     s.Category_Name,
                                     s.Category_Description,


                                 }).ToListAsync();


            return records;


        }
    }
}