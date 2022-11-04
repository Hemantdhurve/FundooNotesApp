using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using RepositoryLayer.Entity;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL ilabelBL;
        private readonly FundooContext fundooContext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        
        public LabelController(ILabelBL ilabelBL, FundooContext fundooContext, IDistributedCache distributedCache, IMemoryCache memoryCache)
        {
            this.ilabelBL = ilabelBL;
            this.fundooContext = fundooContext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
           
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

        [Authorize]
        [HttpGet]
        [Route("Redis")]
        //Redis Implementation 
        public async Task<IActionResult> GetAllCollabUsingRedisCache()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var cacheKey = "LabelList";
            string serializedLabelList;
            var LabelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                LabelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                LabelList = fundooContext.LabelTable.ToList();
                serializedLabelList = JsonConvert.SerializeObject(LabelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);

                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(LabelList);
        }
    }
}
