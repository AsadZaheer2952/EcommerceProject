using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model
{
    public class ForgetPassword
    {
        [EmailAddress]
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
