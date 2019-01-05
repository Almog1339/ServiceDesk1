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
    public class HRController : ControllerBase
    {
        [HttpGet]
        public object GetOpenPositions()
        {
            return DBHelper.GetOpenPositions();
        }

        [HttpGet("Candidate")]
        public object GetCandidate()
        {
            return DBHelper.GetCandidate();
        }
        [HttpGet("GetPositionID")]
        public int PositionID()
        {
            return DBHelper.PositionID();
        }
        [HttpPost]
        public bool NewPosition(string JobTitle,string JobDescription,string Department)
        {
            if (string.IsNullOrEmpty(JobTitle)||string.IsNullOrEmpty(JobDescription)) 
            {
                return false;
            }
            else {
                return DBHelper.NewPosition(JobTitle, JobDescription, Department);
            }
        }
    }
}