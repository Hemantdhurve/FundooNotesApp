using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using RepositoryLayer.Context;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Entity;
using Microsoft.Extensions.Logging;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL inotesBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;
        private readonly long noteId;

        private readonly ILogger<NotesController> logger;

        public NotesController(FundooContext fundooContext, INotesBL inotesBL, IMemoryCache memoryCache, IDistributedCache distributedCache, ILogger<NotesController> logger)
        {

            this.fundooContext = fundooContext;
            this.inotesBL = inotesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.logger = logger;

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
        [HttpGet]
        [Route("RetrieveAll")]

        public IActionResult RetrieveAllNotes()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.RetrieveAllNotes(userId);
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
        [HttpDelete]
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
        //HttpPut method is used to update the notes when pinned.
        public IActionResult PinNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.PinNote(noteId,userId);

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

        [Authorize]
        [HttpPut]
        [Route("Archieve")]
        public IActionResult ArchieveNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.ArchieveNote(noteId, userId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Archieved Successfully " });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note Archieve UnSuccessful" });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Trash")]
        public IActionResult TrashNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.TrashNote(noteId, userId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Moved to Bin Successfully " });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note not Moved to Bin" });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Image")]

        public IActionResult ImageNotes(IFormFile Image,long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.ImageNotes(Image, noteId, userId);
                if (result!=null)
                {
                    return Ok(new { success = true, message = "Image Added Successfully " });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Image Addition UnSuccessful" });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Color")]
        public IActionResult BackgroundColorNote(long noteId,string backgroundcolor)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inotesBL.BackgroundColorNote(noteId,backgroundcolor);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Background Color Changed Successfully " });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Background Color Change Unsuccessful" });
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
        [Authorize]
        [HttpGet]
        [Route("Redis")]
        //Redis Implementation 
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            //both lines should be used to get all the data from Redis but in Postman it works without these lines
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var result = inotesBL.RetrieveNotes(userId, noteId);
            
            var cacheKey = "NotesList";
            string serializedNotesList;
            var NotesList = new List<NotesEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                NotesList = fundooContext.NotesTable.ToList();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }
    }
}
