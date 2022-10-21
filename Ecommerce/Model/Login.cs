using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model
{
    public class Login
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        
    }
}
