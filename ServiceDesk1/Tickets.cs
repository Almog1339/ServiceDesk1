using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk1
{
    public class Tickets
    {

        public int ID { get; set; }
        public string Open_by { get; set; }
        public string Short_Description { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }
        public string State { get; set; }
        public string Category { get; set; }
        public Int16 Impact { get; set; }
        public Int16 Urgency { get; set; }
        public string Assigned_to { get; set; }
        public Tickets(int ID, string Open_by, string Short_Description, string Description, string GroupName, string State, string Category, Int16 Impact, Int16 Urgency, string Assigned_to)
        {
            this.ID = ID;
            this.Open_by = Open_by;
            this.Short_Description = Short_Description;
            this.Description = Description;
            this.GroupName = GroupName;
            this.State = State;
            this.Category = Category;
            this.Impact = Impact;
            this.Urgency = Urgency;
            this.Assigned_to = Assigned_to;
        }
        public Tickets()
        {

        }

        public Tickets(int ID, string Open_by, string Short_Description, string Description, string GroupName, string State, string Category, Int16 Impact, Int16 Urgency)
        {
            this.ID = ID;
            this.Open_by = Open_by;
            this.Short_Description = Short_Description;
            this.Description = Description;
            this.GroupName = GroupName;
            this.State = State;
            this.Category = Category;
            this.Impact = Impact;
            this.Urgency = Urgency;
        }
    }
}
