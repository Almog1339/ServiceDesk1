using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk1
{
    public class Knowledge
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public int PostedBy { get; set; }
        public string PostedByName { get; set; }
        public string Title { get; set; }

        public Knowledge(int ID,string subject, string Content,int postedBy,string PostedByName,string tital)
        {
            this.ID = ID;
            this.Subject = subject;
            this.Content = Content;
            this.PostedBy = postedBy;
            this.PostedByName = PostedByName;
            this.Title = tital;
        }
        public Knowledge(string subject)
        {
            this.Subject = subject;
        }
        public Knowledge()
        {

        }

    }
}
