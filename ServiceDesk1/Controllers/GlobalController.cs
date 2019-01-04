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
    public class GlobalController : ControllerBase
    {
        [HttpGet("UsersList")]
        public object UsersList()
        {
            return DBHelper.UsersList();
        }

        [HttpGet("Department")]
        public object DepartmentList()
        {
            return DBHelper.DepartmentList();
        }

        [HttpGet("info")]
        public object info(string loginID)
        {
            return DBHelper.getInfo(loginID);
        }

        [HttpPost("info")]
        public bool updateInfo(string firstName, string lastName,int BusinessEntityID)
        {
            return DBHelper.updateInfo(firstName,lastName, BusinessEntityID);
        }
    }
}