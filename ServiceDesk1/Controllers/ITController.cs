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
        [HttpGet("GetUnassigned")]
        public object GetUnassiged()
        {
            return DBHelper.GetUnassiged();
        }
        [HttpGet("GetOpenTicket")]
        public object GetOpenTicket()
        {
            return DBHelper.GetOpenTicket();
        }
        [HttpGet("ResolveInc")]
        public object ResolveInc()
        {
            
            return DBHelper.ResolveInc();
        }
        [HttpGet("AssignToMe")]
        public object AssignToMe(string username)
        {
            if (string.IsNullOrEmpty(username)) {
                return -1;
            }
            return DBHelper.AssignToMe(username);
        }
        [HttpPost("SubmitNewTicket")]
        public object SubmitNewTicket(string LoginId,string category,string shortDescription,string description)
        {
            if (string.IsNullOrEmpty(LoginId)|| string.IsNullOrEmpty(category)|| string.IsNullOrEmpty(shortDescription)||string.IsNullOrEmpty(description)) {
                return -1;
            }return DBHelper.SubmitNewTicket(LoginId, category, shortDescription, description);
        }
    }
}