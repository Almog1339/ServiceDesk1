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
        [HttpPost]
        public static bool PostNewArticle(int ID,string title,string content)
        {
            if (string.IsNullOrEmpty(title)||string.IsNullOrEmpty(content)||ID.Equals(null))
            {
                return false;
            }
            else
            {
                return DBHelper.PostNewArticle(ID, title, content);
            }
            
        }

        [HttpGet]
        public static int GetID()
        {
            return DBHelper.GetID();
        }
    }
}