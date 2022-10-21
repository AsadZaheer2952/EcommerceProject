using Ecommerce.Data;
using Ecommerce.Migrations;
using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Repository
{
    public class AccountRepository : IAccountRepo

    {
        private readonly EcommStoreContext _context;
        public readonly IConfiguration _configuration;
        public AccountRepository(EcommStoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }
        public async Task<object> SignUp(SignUpModel signUpModel)
        {
            var records = await _context.SignUp.Where(i => i.Email == signUpModel.Email).FirstOrDefaultAsync();
            if(records!= null)
            {
                return new
                {
                    Message = " Email Already exist"
                };
            }
            var signup = new SignUpModel()
            {
               
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.Email,
                Password = signUpModel.Password,
                ConfirmPassword = signUpModel.ConfirmPassword,
                Role = signUpModel.Role
            };
            _context.SignUp.Add(signup);
            await _context.SaveChangesAsync();
            return signup;
        }
        public async Task<Tokens> LoginUser(Login signUp)
        {
            var user = await _context.SignUp.Where(u => u.Email == signUp.Email && u.Password == signUp.Password).FirstOrDefaultAsync();
            if (user == null)
            {
                return new Tokens { };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                  {
             new Claim(ClaimTypes.Email, user.Email),
             new Claim(ClaimTypes.Role, user.Role)

                  }),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };



        }
        public async Task<SignUpModel> GetProfile(string Email)
        {
            var profile = await _context.SignUp.Where(i => i.Email == Email).FirstOrDefaultAsync();
            return (profile);
        }
        public async Task<int> ForgetPassword(ForgetPassword forgetPassword)
        {
            var user = await _context.SignUp.Where(x => x.Email == forgetPassword.UserEmail).FirstOrDefaultAsync();
            if (user == null)
            {
                return 0;
            }
            if (forgetPassword.Password != forgetPassword.ConfirmPassword)
                return 0;

            user.Password = forgetPassword.Password;
            user.ConfirmPassword = forgetPassword.ConfirmPassword;
            _context.SignUp.Update(user);
            await _context.SaveChangesAsync();
            return 1;
        }
        public async Task<UpdateProfile> UpdateProfile(UpdateProfile update, string email)
        {
            var records = await _context.SignUp.Where(i => i.Email == email).FirstOrDefaultAsync();
            if (records != null)
            {


                records.FirstName = update.FirstName;
                records.LastName = update.LastName;


                _context.SignUp.Update(records);
                await _context.SaveChangesAsync();



            }
            return update;
        }
        public async Task<List<SignUpModel>> GetAllUser()
        {
            var userrecord= await _context.SignUp.Select(x => new SignUpModel()
            {
                User_Id = x.User_Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password,
                ConfirmPassword = x.ConfirmPassword,
                Role = x.Role

            }).ToListAsync();
            return (userrecord);
        }
        public async Task DeleteUser( string Email)
        {
             var delete = _context.SignUp.Where(i => i.Email == Email).FirstOrDefault();


            _context.SignUp.Remove(delete);
            await _context.SaveChangesAsync();


        }
 /*       public async Task <object> AddUser(SignUpModel signUpModel)
        {
            var res = _context.SignUp.Where(i => i.Role == "Admin").FirstOrDefault();
         
            var signup = new SignUpModel()
            {

                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.Email,
                Password = signUpModel.Password,
                ConfirmPassword = signUpModel.ConfirmPassword,
                Role = signUpModel.Role
            };
            _context.SignUp.Add(signup);
            await _context.SaveChangesAsync();
            return signup;
        }*/
      


    }
}
