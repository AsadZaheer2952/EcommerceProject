using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ?CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public List <ProductCategories> ? Productcategories { get; set; }
        public Guid CategoryId { get; set; }

    }
}
