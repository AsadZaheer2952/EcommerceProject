using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        public string Category_Name { get; set; }
        public string Category_Description { get; set; }
         public int ParentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public List <ProductCategories>? productsCategories { get; set; }

    }
}
