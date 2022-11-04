using Ecommerce.Model;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public interface IAccountRepo
    {
        Task<object> SignUp(SignUpModel signUpModel);
        Task<Tokens> LoginUser(Login signUp);
        Task<SignUpModel> GetProfile(string Email);
        Task<int> ForgetPassword(ForgetPassword forgetPassword);
        Task<UpdateProfile> UpdateProfile( UpdateProfile update, string email);
        Task<List<SignUpModel>> GetAllUser();
        Task DeleteUser( string Email);
        Task<List<SignUpModel>> GetAllUsers();
  


    }

}
