﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceDesk1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        [HttpGet("image")]
        public object image(string loginID) => DBHelper.GetImg(loginID);

        [HttpGet]
        public object ListOrganizerCtrl(int departmentId) => DBHelper.GetListOfOption(departmentId);
    }
}