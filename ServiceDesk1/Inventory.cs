using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ServiceDesk1
{
    public class Inventory
    {

        public int SupplierID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string HomePage { get; set; }
        //Orders
        public int OrderID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime Required_Date { get; set; }
        public DateTime Shipped_Date { get; set; }
        public int Freight { get; set; }
        public string Ship_Name { get; set; }
        public string Ship_Address { get; set; }
        public string Ship_City { get; set; }
        public string Ship_Country { get; set; }
        public string Employee_FirstName { get; set; }
        public string Employee_LastName { get; set; }


        public  Inventory(int SupplierID, string CompanyName,string ContactName, string ContactTitle, string Address, string City,string PostalCode,string Country,string Phone,string Fax,string HomePage)
        {
            this.SupplierID = SupplierID;
            this.CompanyName = CompanyName;
            this.ContactName = ContactName;
            this.ContactTitle = ContactTitle;
            this.Address = Address;
            this.City = City;
            this.PostalCode = PostalCode;
            this.Country = Country;
            this.Phone = Phone;
            this.Fax = Fax;
            this.HomePage = HomePage;
        }
        public Inventory(int OrderID,int EmployeeID,string Employee_FirstName,string Employee_LastName, DateTime OrderDate, DateTime Required_Date, DateTime Shipped_Date, int Freight, string Ship_Name, string Ship_Address, string Ship_City, string Ship_Country)
        {
            this.OrderID = OrderID;
            this.EmployeeID = EmployeeID;
            this.Employee_FirstName = Employee_FirstName;
            this.Employee_LastName = Employee_LastName;
            this.OrderDate = OrderDate;
            this.Required_Date = Required_Date;
            this.Shipped_Date = Shipped_Date;
            this.Freight = Freight;
            this.Ship_Name = Ship_Name;
            this.Ship_Address = Ship_Address;
            this.Ship_City = Ship_City;
            this.Ship_Country = Ship_Country;
        }
        

    }
}
