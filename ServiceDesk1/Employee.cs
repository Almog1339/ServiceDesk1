using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk1
{
    public class Employee
    {
        public int BusinessEntityID { get; set; }
        public int NationalIDNumber { get; set; }
        public string Password { get; set; }
        public string LoginID { get; set; }
        public int OrganizationLevel { get; set; }
        public string JobTitle { get; set; }
        public DateTime BirthDate { get; set; }
        public char MaritalStatus { get; set; }
        public char Gender { get; set; }
        public DateTime HireDate { get; set; }
        public int SalariedFlag { get; set; }
        public int VacationHours { get; set; }
        public int SickLeaveHours { get; set; }
        public int CurrentFlag { get; set; }
        public int Rowguid { get; set; }
        public static int DepartmentID { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
