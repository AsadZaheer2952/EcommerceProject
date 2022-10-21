using Ecommerce.Model;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class EcommStoreContext : DbContext
    {

        public EcommStoreContext(DbContextOptions<EcommStoreContext> option) : base(option)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategories> ProductCategories { get; set; }
        
       
        public DbSet<SignUpModel> SignUp { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
              new Category
              {
                  CategoryId = Guid.NewGuid(),
                  Category_Name = "test",
                  Category_Description = "test",
                  CreatedAt = DateTime.UtcNow,
                  CreatedBy = "Asad",
                  UpdatedAt = DateTime.UtcNow,
                  UpdatedBy = "asad",
                  DeletedAt = DateTime.UtcNow,
                  DeletedBy ="Asad",


              },
                  new Category()
                  {
                      CategoryId = Guid.NewGuid(),
                      Category_Name = "Category_Name",
                      Category_Description = "abc",
                      CreatedAt = DateTime.UtcNow,
                      CreatedBy = "Asad",
                      UpdatedAt = DateTime.UtcNow,
                      UpdatedBy = "asad",
                      DeletedAt = DateTime.UtcNow,
                      DeletedBy = "Asad",

                  }
              );
        }
    }
}
