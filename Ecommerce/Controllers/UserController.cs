

using Ecommerce.Model;
using Ecommerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class UserController : ControllerBase
    {
        private readonly IAccountRepo _accountRepo;
        
        public UserController(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
            
           
        }
        [HttpGet("GetAll"), Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAll()
        {
       
            var users = await _accountRepo.GetAllUser();
            return Ok(users);
        }
      

        [HttpPost("Register")]
        public async Task<IActionResult> Adds([FromBody] SignUpModel signUpModel)
        {
            var user = await _accountRepo.SignUp(signUpModel);
            return Ok(user);
        }
        [HttpPost("/register/Admin"), Authorize( Roles= "SuperAdmin")]
        public async Task<IActionResult> AddsAdmin([FromBody] SignUpModel signUpModel)
        {
            var user = await _accountRepo.SignUp(signUpModel);
            return Ok(user);
        }
        [HttpPost("/register/User"), Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> AddsUSer([FromBody] SignUpModel signUpModel)

        {
            string role = (this.User.Claims.First(i => i.Type == ClaimTypes.Role).Value);
            var user = await _accountRepo.SignUp(signUpModel);
            return Ok(user);
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Login([FromBody] Login signUp)
        {
            
            var login = await _accountRepo.LoginUser(signUp );
            
            var cookieOption = new Microsoft.AspNetCore.Http.CookieOptions()
            {
                Path = "/",
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(3),
            };
            Response.Cookies.Append("accessToken", login.Token, cookieOption);
            return Ok(login);
        }
        [Authorize(Roles = "SuperAdmin,Admin,User")]
        [HttpGet("getProfile")]
        public async Task<IActionResult> GetProfiles()
        {
            string email = (this.User.Claims.First(i => i.Type == ClaimTypes.Email).Value);
            var prof = await _accountRepo.GetProfile(email);
            return Ok(prof);
        }
        [Authorize(Roles = "SuperAdmin,Admin,User")]
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ForgetPassword forgetPassword)
        {
            var change = await _accountRepo.ForgetPassword(forgetPassword);
            return Ok("Password Reset Successfully!");

        }
        [Authorize(Roles = "SuperAdmin,Admin,User")]
        [HttpPut("UpdateProfile")]    
        public async Task<IActionResult> Update([FromBody] UpdateProfile update)
            {
            string email = (this.User.Claims.First(i => i.Type == ClaimTypes.Email).Value);
            var result =await _accountRepo.UpdateProfile(update,email);
            return Ok(result);
         }
        [HttpDelete("DeleteUser"), Authorize (Roles ="SuperAdmin,Admin,User")]
        public async Task<IActionResult> Delete([FromQuery] string Email)
        {
            await _accountRepo.DeleteUser(Email);
            return Ok("User Deleted Successfully!");
        }
        [Authorize(Roles = "SuperAdmin,Admin,User")]
        [HttpPost("Logout")]
        public async Task<IActionResult>LogOut()
        {
            try
            {
                var res = Response.Cookies;
                if (res == null)
                    return BadRequest("Unauthorized");
                Response.Cookies.Delete("accessToken");
                return Ok("Logout successfully");
            }
            catch(Exception ex)
            {
                return StatusCode(401,ex);
            }
        }

            
    }
}