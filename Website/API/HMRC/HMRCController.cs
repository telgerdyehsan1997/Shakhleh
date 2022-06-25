using Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Olive;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Website.API
{
    [Route("api/v1/hmrc")]
    [ApiController]
    [HMRCAuthorization]
    public class HMRCController : ControllerBase
    {
        public HMRCController()
        {
        }
    }
}
