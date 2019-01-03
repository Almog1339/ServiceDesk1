using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk1
{
    public class Message
    {
        public string Content { get; set; }
        public string LoginID { get; set; }
        public int MessageID { get; set; }

        public Message(string Content, string LoginID, int MessageID)
        {
            this.Content = Content;
            this.LoginID = LoginID;
            this.MessageID = MessageID;
        }
    }
}
