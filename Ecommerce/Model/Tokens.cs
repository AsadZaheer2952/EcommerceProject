using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model
{
    public class Tokens
    {
        [Key]
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
