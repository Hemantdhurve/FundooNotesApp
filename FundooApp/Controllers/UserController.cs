using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    }
}
