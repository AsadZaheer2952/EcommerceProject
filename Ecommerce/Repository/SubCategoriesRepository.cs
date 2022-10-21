using Ecommerce.Data;
using Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class SubCategoriesRepository : ISubCategories
    {
        private readonly EcommStoreContext _context;
        public SubCategoriesRepository(EcommStoreContext context)
        {
            _context = context;

        }
     /*   public async Task<ProductCategories> AddProductCategories(Guid ProductId, Guid CategoryId)
        {
          //  var result = _context.Products.Where(x.)
            var Productcategories = new ProductCategories()
            {   
                ProductId= ProductId,
                CategoryId= CategoryId,
                CreatedAt = DateTime.UtcNow,
               *//* //CreatedBy = product.CreatedBy,
                //UpdatedAt = DateTime.UtcNow,
                //UpdatedBy = product.UpdatedBy,
                //DeletedAt = DateTime.UtcNow,
                //DeletedBy = product.DeletedBy,*//*
            };
            _context.ProductCategories.Add(Productcategories);
            await _context.SaveChangesAsync();
            return Productcategories;

        }*/
        public async Task<List<ProductCategories>>GetAllSubCategories()
        { 
                var results = await _context.ProductCategories.Select(x => new ProductCategories()
                {
                    ProductId = x.ProductId,
                    CategoryId = x.CategoryId,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                    DeletedAt = x.DeletedAt,
                    DeletedBy = x.DeletedBy
                }).ToListAsync();
            return (results);
        }


}
}
