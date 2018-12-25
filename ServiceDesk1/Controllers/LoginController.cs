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
        public bool Login([FromForm]Employee userData)
        {
            if (string.IsNullOrEmpty(userData.LoginID) || string.IsNullOrEmpty(userData.Password))
            {
                return false;
            }
            Employee employee = new Employee {
                LoginID = userData.LoginID,
                Password = userData.Password
            };
            return DBHelper.ValidateUser(userData.LoginID, userData.Password);
        }

        [HttpGet]
        public short GetDepartmentID(Employee userData)
        {
            if (string.IsNullOrEmpty(userData.LoginID))
            {
                return -1;
            }
            
            return DBHelper.ValidateDepartment(userData.LoginID);
        }
    }

}
