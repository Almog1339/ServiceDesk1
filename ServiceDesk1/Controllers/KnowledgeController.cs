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
    public class KnowledgeController : ControllerBase
    {
        [HttpGet("EntityID")]
        public int EntityID(string userName)
        {
            return DBHelper.GetBEID(userName);
        }

        [HttpGet]
        public int GetID()
        {
            return DBHelper.GetID();
        }
        [HttpPost]
        public bool submitArticle(int ID, string title, string content,int BusinessEntityID)
        {
            if (string.IsNullOrEmpty(title)||string.IsNullOrEmpty(content)||ID != -1 || ID != 0 || BusinessEntityID != 0|| BusinessEntityID != -1)
            {
                return false;
            }

            return DBHelper.PostNewArticle(ID, title, content, BusinessEntityID);
        }
    }
}