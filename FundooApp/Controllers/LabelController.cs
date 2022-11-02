using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using RepositoryLayer.Entity;

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
        [Authorize]
        [HttpGet]
        [Route("Retrieve")]

        public IActionResult RetrieveLabel(long labelId)
        {
            try
            { 
                var result = ilabelBL.RetrieveLabel(labelId);

                if (result != null)
                {

                    return Ok(new { success = true, message = "Label Retrieve Successful ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label Retrieve UnSuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete")]

        public IActionResult DeleteLabel(long labelId)
        {
            try
            {

                var result = ilabelBL.DeleteLabel(labelId);

                if (result != null)
                {

                    return Ok(new { success = true, message = "Label Deleted Successful " });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label Deletion UnSuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Edit")]

        public IActionResult EditLabel(long notesId, string labelName)
        {
            try
            {
                var result = ilabelBL.EditLabel(notesId,labelName);

                if (result != null)
                {

                    return Ok(new { success = true, message = "Label Updated Successful ",data = result});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label updation UnSuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
