using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceDesk1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public short Login([FromForm]Employee userData)
        {
            if (string.IsNullOrEmpty(userData.UserName) || string.IsNullOrEmpty(userData.Pass))
            {
                return -1;
            }

            return DBHelper.ValidateUser(userData.UserName, userData.Pass);
        }
    }

}
