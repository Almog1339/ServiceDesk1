using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk1
{
    public class Knowledge
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int PostedBy { get; set; }
        public string PostedByName { get; set; }

        public Knowledge(int ID,string Title,string Content,string PostedByName)
        {
            this.ID = ID;
            this.Title = Title;
            this.Content = Content;
            this.PostedByName = PostedByName;
        }

        public Knowledge()
        {

        }

    }
}
