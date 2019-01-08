using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery.Internal;

namespace ServiceDesk1
{
    public class Employee
    {
        public int BusinessEntityID { get; set; }
        public int NationalIDNumber { get; set; }
        public string Password { get; set; }
        public string LoginID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int OrganizationLevel { get; set; }
        public string JobTitle { get; set; }
        public DateTime BirthDate { get; set; }
        public char MaritalStatus { get; set; }
        public char Gender { get; set; }
        public DateTime HireDate { get; set; }
        public int SalariedFlag { get; set; }
        public int VacationHours { get; set; }
        public int SickLeaveHours { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }
        public int CurrentFlag { get; set; }
        public int Rowguid { get; set; }
        public static int DepartmentID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Img { get; set; }

        public Employee()
        {

        }
        public Employee (int BusinessEntityID, string FirstName,string LastName,string JobTitle,string LoginID,string EmailAddress,string PhoneNumberType,string PhoneNumber)
        {
            this.BusinessEntityID = BusinessEntityID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.JobTitle = JobTitle;
            this.LoginID = LoginID;
            this.EmailAddress = EmailAddress;
            this.PhoneNumberType = PhoneNumberType;
            this.PhoneNumber = PhoneNumber;
        }
        public Employee(int BusinessEntityID, string FirstName, string LastName, string JobTitle, string LoginID, string EmailAddress, string PhoneNumberType, string PhoneNumber,string Img)
        {
            this.BusinessEntityID = BusinessEntityID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.JobTitle = JobTitle;
            this.LoginID = LoginID;
            this.EmailAddress = EmailAddress;
            this.PhoneNumberType = PhoneNumberType;
            this.PhoneNumber = PhoneNumber;
            this.Img = Img;
        }
        public Employee(string firstName, string lastName, string phoneNumber, string jobTitle, string emailAddress,int businessEntityId)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.JobTitle = jobTitle;
            this.EmailAddress = emailAddress;
            this.BusinessEntityID = businessEntityId;
        }
        public Employee(string Img)
        {
            this.Img = Img;
        }
    }
}
