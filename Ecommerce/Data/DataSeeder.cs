using Ecommerce.Data;
using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class DataSeeder
    {
        private readonly EcommStoreContext ecommStoreContext;

        public DataSeeder(EcommStoreContext ecommStoreContext)
        {
            this.ecommStoreContext = ecommStoreContext;
        }

        public void seed()
        {
            if (!ecommStoreContext.Categories.Any())
            {
                var Categories = new List <Category>()
                { new Category
                    {
                        Category_Name = "new-1",
                        Category_Description = "abc",
                        CategoryId = Guid.NewGuid(),
                    },
                    new Category()
                    {
                        Category_Name = "new-1",
                        Category_Description = "abc",
                        CategoryId= Guid.NewGuid(),
                    }
                };
                ecommStoreContext.Categories.AddRange(Categories);
                ecommStoreContext.SaveChanges();
            }
        }
    }

}