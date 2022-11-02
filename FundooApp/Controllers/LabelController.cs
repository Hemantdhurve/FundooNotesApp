using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL ilabelBL;


        public LabelController(ILabelBL ilabelBL)
        {
            this.ilabelBL = ilabelBL;

        }

        [Authorize]
        [HttpPost]
        [Route("Create")]

        public IActionResult CreateLabel(long notesId, string labelName)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = ilabelBL.CreateLabel(notesId,userId, labelName);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Lable Creation Successful ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Lable Creation UnSuccessful" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }


    }
}
