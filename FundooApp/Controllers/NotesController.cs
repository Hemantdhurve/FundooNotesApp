using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL inotesBL;
        private readonly long userId;
        private readonly long notesId;

        public NotesController(INotesBL inotesBL)
        {
            this.inotesBL = inotesBL;
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]

        public IActionResult CreateNotes(NotesModel notesModel)
        {
            try
            {
                long userId= Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.CreateNotes(notesModel,userId);

                if (result != null)
                {
                    
                    return Ok(new { success = true, message = "Notes Creation Successful ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Notes Creation UnSuccessful" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("Retrieve/{noteId}")]

        public IActionResult RetrieveNotes(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.RetrieveNotes(userId,noteId);
                if (result != null)
                {
                    return Ok(new {success = true, message = "Retrieve data Successful ", data = result});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Retrieve data UnSuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
