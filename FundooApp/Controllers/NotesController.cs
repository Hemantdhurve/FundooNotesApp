using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using RepositoryLayer.Context;
using System.Threading.Tasks;

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
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.CreateNotes(notesModel, userId);

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
        [Route("Retrieve")]

        public IActionResult RetrieveNotes(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.RetrieveNotes(userId, noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Retrieve data Successful ", data = result });
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

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateNote(long noteId, NotesModel notesModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.UpdateNote(userId,noteId,notesModel);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Update data Successful ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Update data UnSuccessful" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Delete")]
        public IActionResult DeleteNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.DeleteNote(userId,noteId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Deletion Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Deletion UnSuccessful" });
                }
            }
            catch (Exception)
            {
                throw ;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Pin")]
        public IActionResult PinNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.PinNote(noteId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Pinned Successful "});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note Pinned UnSuccessful" });
                }
            }
            catch (Exception e)
            {

                throw e;  
            }
        }
    }
}
