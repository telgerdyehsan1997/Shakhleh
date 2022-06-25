using Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Olive;
using Olive.Entities.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Website.API
{
    [Route("api/v1/gvms")]
    [ApiController]
    public class GVMSController : ControllerBase
    {

        public GVMSController()
        {
        }


        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpGet, Route("notification")]
        public async Task<IActionResult> GetNotifications()
        {
            try
            {
                return Ok(new { challenge = Request.Query["challenge"].ToStringOrEmpty() });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
