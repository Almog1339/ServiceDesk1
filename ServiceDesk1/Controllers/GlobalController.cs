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
        public object UsersListWithPic()
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
        [HttpGet("GetImg")]
        public object GetImg(string loginID)
        {
            if (string.IsNullOrEmpty(loginID)) {
                return -1;
            }
            return DBHelper.GetImg(loginID);
        }
        [HttpGet("GetAllImg")]
        public object GetAllImg()
        {
            return DBHelper.GetAllImg();
        }
        [HttpGet("GetTime")]
        public object GetTime()
        {
            return DBHelper.GetDateTime();
        }
        [HttpPost("info")]
        public bool UpdateInfo(string firstName, string lastName,int BusinessEntityID)
        {
            if (string.IsNullOrEmpty(firstName)||string.IsNullOrEmpty(lastName))
            {
                return false;
            }
            return DBHelper.updateInfo(firstName,lastName, BusinessEntityID);
        }

        [HttpPost("passwordReset")]
        public bool passwordReset(string loginID, string password,string newPassword,int BusinessEntityID)
        {
            if (string.IsNullOrEmpty(loginID) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(newPassword)) {
                return false;
            }
            else {
                return DBHelper.passwordReset(loginID, password, newPassword, BusinessEntityID);
            }
        }
    }
}