using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk1
{
    public class Chats
    {

        public int Room_ID { get; set; }
        public int User1 { get; set; }
        public int User2 { get; set; }
        public string Message { get; set; }
        public int Message_ID { get; set; }
        public string LoginID { get; set; }
        

        public Chats(int Room_ID, int User1, int User2)
        {
            this.Room_ID = Room_ID;
            this.User1 = User1;
            this.User2 = User2;
        }
    }
}
