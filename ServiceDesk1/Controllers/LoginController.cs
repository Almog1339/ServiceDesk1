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
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public bool Login([FromForm]Employee userData)
        {
            if (string.IsNullOrEmpty(userData.LoginID) || string.IsNullOrEmpty(userData.Password))
            {
                return false;
            }
            return DBHelper.ValidateUser(userData.LoginID, userData.Password);
        }

        [HttpGet]
        public short GetDepartmentId(string loginID)
        {
            if (string.IsNullOrEmpty(loginID))
            {
                return -1;
            }
            
            return DBHelper.ValidateDepartment(loginID);
        }
        
    }

}
