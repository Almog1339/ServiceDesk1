using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceDesk1.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [Microsoft.AspNetCore.Mvc.HttpPost]
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
        
        public short GetDepartmentId([FromBody]string result)
        {
            if (string.IsNullOrEmpty(result))
            {
                return -1;
            }
            
            return DBHelper.ValidateDepartment(result);
        }
        
    }

}
