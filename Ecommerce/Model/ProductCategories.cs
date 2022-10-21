using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model
{
    public class ProductCategories
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }

        

        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }
}
