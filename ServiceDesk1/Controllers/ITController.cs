using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceDesk1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ITController : ControllerBase
    {
        [HttpGet("GetNewInc")]
        public int GetNewInc()
        {
            return DBHelper.GetNewInc();
        }
    }
}