using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iuserBL;
        public UserController(IUserBL iuserBL)
        {
            this.iuserBL = iuserBL;
        }
        //API call Should Happen using HTTPPOST
        [HttpPost]
        [Route("Register")]          //

        public IActionResult RegisterUser(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result=iuserBL.Registration(userRegistrationModel);

                if (result != null)
                {
                    //ok is HTTP respnse it has some format and is called 
                    return Ok(new {success=true,message="Registration is Successful",data=result});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration is UnSuccessful" });
                }
            
            }
            catch(System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("Login")]

        public IActionResult LoginUser(UserLoginModel userLoginModel)
        {
            try
            {
                var resultLog = iuserBL.Login(userLoginModel);

                if (resultLog != null)
                {
                    return Ok(new { success = true, message = "Login Successful", data =  resultLog });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login UnSuccessful" });
                }

            }
            catch(System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]

        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var resultLog = iuserBL.ForgetPassword(email);

                if (resultLog != null)
                {
                    return Ok(new { success = true, message = "Reset Email Send"});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset UnSuccessful" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
     
        public IActionResult ResetPassword(string newPassword,string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var resultLog = iuserBL.ResetPassword(email,newPassword, confirmPassword);

                if (resultLog != null)
                {
                    return Ok(new { success = true, message = "Password Reset Successful"});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Password Reset UnSuccessful" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
