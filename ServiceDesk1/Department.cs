using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk1.wwwroot
{
    public class Department
    {
        public int ID { get; set; }
        public string DepartmentName { get; set; }
        public string GroupName { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
        public string ManagerLoginID { get; set; }
        public string ManagerEmail { get; set; }
        public string ManagerTitle { get; set; }
        public string ManagerPhoneNumber { get; set; }

        public Department()
        {

        }
        public Department(int ID, string DepartmentName, string GroupName, string ManagerFirstName, string ManagerLastName, string ManagerTitle,
            string ManagerLoginID, string ManagerEmail, string ManagerPhoneNumber)
        {
            this.ID = ID;
            this.DepartmentName = DepartmentName;
            this.GroupName = GroupName;
            this.ManagerFirstName = ManagerFirstName;
            this.ManagerLastName = ManagerLastName;
            this.ManagerTitle = ManagerTitle;
            this.ManagerLoginID = ManagerLoginID;
            this.ManagerEmail = ManagerEmail;
            this.ManagerPhoneNumber = ManagerPhoneNumber;
        }
    }
    
}
