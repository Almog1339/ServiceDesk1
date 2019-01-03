using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk1
{
    public class Chats
    {
        public int ChatId { get; set; }
        public string ChatWithLoginId { get; set; }
        public DateTime LastMessageTime { get; set; }

        public Chats()
        {

        }

        public Chats(int ChatID, string ChatWithLoginId, DateTime LastMessageTime)
        {
            this.ChatId = ChatID;
            this.ChatWithLoginId = ChatWithLoginId;
            this.LastMessageTime = LastMessageTime;
        }
    }
}
