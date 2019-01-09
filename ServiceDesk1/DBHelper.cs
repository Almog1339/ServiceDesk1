using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Antiforgery.Internal;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Newtonsoft.Json;
using ServiceDesk1.wwwroot;

namespace ServiceDesk1
{
    public class DBHelper
    {
        private static string CONN_STRING =
            "Data Source=DESKTOP-O8IU0PQ\\SQLEXPRESS;Initial Catalog=ServiceDesk;Persist Security Info=True;User ID=sa;Password=Q1w2q1w2";

        internal static object GetUnassiged()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" select ID,Open_by,Short_Description,Description,GroupName,State,Category,Impact,Urgency,Assigned_to from Incidents where Assigned_to = ' '", conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        List<Tickets> tickets = new List<Tickets>();
                        while (dr.Read()) {
                            Tickets ticket = new Tickets(dr.GetInt32(0),dr.GetString(1),dr.GetString(2),dr.GetString(3),dr.GetString(4),dr.GetString(5),dr.GetString(6),dr.GetInt16(7),dr.GetInt16(8),dr.GetString(9));
                            tickets.Add(ticket);
                            
                        }
                        return tickets;
                    }
                    
                }
            }
        }
        internal static object GetOpenTicket()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" select ID,Open_by,Short_Description,Description,GroupName,State,Category,Impact,Urgency,Assigned_to from Incidents where State = 'open'", conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        List<Tickets> tickets = new List<Tickets>();
                        while (dr.Read()) {
                            Tickets ticket = new Tickets(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetInt16(7), dr.GetInt16(8), dr.GetString(9));
                            tickets.Add(ticket);

                        }
                        return tickets;
                    }

                }
            }
        }
        public static int PositionID()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" select max(JobID) from Positions", conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())    {
                        if (dr.Read()) {
                            int id = dr.GetInt32(0);
                            id++;
                            return id;
                        }
                        return -1;
                    }
                }               
            }
        }

        internal static List<Employee> GetAllImg()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand("Select ImgData from Images",conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        List<Employee> Images = new List<Employee>();
                        while (dr.Read()) {
                            Employee img = new Employee(dr.GetString(0));
                            Images.Add(img);
                        }
                        return Images;
                    }
                }
            }
        }

        public static int ReutnDepartmentID(string Department)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand("select DepartmentID from HumanResources.Department where name = @Department", conn)) {
                    cmd.Parameters.AddWithValue("@Department", Department);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        if (dr.Read()) {
                            return dr.GetInt16(0);
                        }
                        return -1;
                    }
                }
            }
        }
        public static string ReturnManagerDepartmnetName(string Department)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" SELECT HumanResources.Employee.LoginID FROM HumanResources.Department INNER JOIN HumanResources.EmployeeDepartmentHistory ON HumanResources.EmployeeDepartmentHistory.DepartmentID = HumanResources.Department.DepartmentID INNER JOIN HumanResources.Employee ON HumanResources.EmployeeDepartmentHistory.BusinessEntityID = HumanResources.Employee.BusinessEntityID WHERE HumanResources.Employee.OrganizationLevel = 1 AND Department.Name = @Department", conn)) {
                    cmd.Parameters.AddWithValue("@Department", Department);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        if (dr.Read()) {
                            return dr.GetString(0);
                        }
                        return "";
                    }
                }
            }
        }

        public static bool NewPosition(string jobTitle, string jobDescription, string department)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                int DepartmentID = ReutnDepartmentID(department);
                string Manager = ReturnManagerDepartmnetName(department);
                using (SqlCommand cmd = new SqlCommand(" insert into Positions (JobDescription,JobTitle,DepartmentID,Manager,PositionStatus) values (@JobDescription,@JobTitle,@DepartmentID,@Manager,'Open')", conn)) {
                    cmd.Parameters.AddWithValue("@JobDescription", jobDescription);
                    cmd.Parameters.AddWithValue("@jobTitle", jobTitle);
                    cmd.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                    cmd.Parameters.AddWithValue("@Manager", Manager);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        if (dr.Read()) {
                            return false;
                        }
                        return true;
                    }
                }
            }
        }

        public static bool ValidateUser(string userName, string pass)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(
                    "SELECT LoginID, PasswordSalt FROM HumanResources.Employee LEFT JOIN PERSON.Password ON HumanResources.Employee.BusinessEntityID = Person.Password.BusinessEntityID WHERE HumanResources.Employee.LoginID = @LoginID AND Person.Password.PasswordSalt = @Password ");
                using (SqlCommand cmd = new SqlCommand(sb.ToString(), conn))
                {
                    cmd.Parameters.AddWithValue("@LoginID", userName);
                    cmd.Parameters.AddWithValue("@Password", pass);

                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    {
                        if (dr.Read())
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        public static bool passwordReset(string loginID, string password, string newPassword,int BusinessEntityID)
        {
            if (ValidateUser(loginID, password) == true)
            {
                using (SqlConnection conn = new SqlConnection(CONN_STRING))
                {
                    using (SqlCommand cmd =
                        new SqlCommand(
                            " update Person.Password set PasswordSalt = '@newPassword' where BusinessEntityID = @BusinessEntityID",
                            conn))
                    {
                        cmd.Parameters.AddWithValue("@newPassword", newPassword);
                        cmd.Parameters.AddWithValue("@BusinessEntityID", BusinessEntityID);
                        conn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                return false;
                            }

                            return true;
                        }
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public static bool PostNewArticle(string Subject, string content, int businessEntityID, string postedByName,string title)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                using (SqlCommand cmd =
                    new SqlCommand(
                        " Insert into Knowledgebase(Subject,Data,PostedBy,PostedByLoginID,Title) values(@Subject,@Content,@BusinessEntityID,@PostedByLoginID)",
                        conn))
                {
                    cmd.Parameters.AddWithValue("@Subject", Subject);
                    cmd.Parameters.AddWithValue("@content", content);
                    cmd.Parameters.AddWithValue("@BusinessEntityID", businessEntityID);
                    cmd.Parameters.AddWithValue("@PostedByLoginID", postedByName);
                    cmd.Parameters.AddWithValue("@Title", title);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }

        public static List<Employee> UsersList()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(
                    " SELECT Person.BusinessEntityID,Person.FirstName, Person.LastName, HumanResources.Employee.JobTitle, Employee.LoginID, EmailAddress, Person.PhoneNumberType.Name, PhoneNumber,[Employee.Photo].ImgData FROM Person.Person left join HumanResources.Employee on HumanResources.Employee.BusinessEntityID = Person.BusinessEntityID left join Person.EmailAddress on HumanResources.Employee.BusinessEntityID = Person.EmailAddress.BusinessEntityID left join Person.PersonPhone on Person.PersonPhone.BusinessEntityID = HumanResources.Employee.BusinessEntityID left join Person.PhoneNumberType on Person.PhoneNumberType.PhoneNumberTypeID = Person.PersonPhone.PhoneNumberTypeID left join[Employee.Photo] on[Employee.Photo].BusinessEntityID = Person.BusinessEntityID WHERE Person.Person.PersonType in ('em', 'sp')",
                    conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        List<Employee> lists = new List<Employee>();
                        while (dr.Read())
                        {
                            Employee list = new Employee(dr.GetInt32(0), dr.GetString(1), dr.GetString(2),
                                dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetString(7),dr.GetString(8));
                            lists.Add(list);
                        }

                        return lists;
                    }
                }
            }
        }
       
        public static List<Knowledge> GetArticles()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(" select ID,title,Data,PostedByLoginID,Subject from Knowledgebase",
                    conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Knowledge> lists = new List<Knowledge>();
                        while (dr.Read())
                        {
                            Knowledge item = new Knowledge(dr.GetInt32(0), dr.GetString(1), dr.GetString(2),
                                dr.GetString(3),dr.GetString(4));
                            lists.Add(item);
                        }
                        return lists;
                    }
                }
            }
        }

        public static List<Department> DepartmentList()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(
                    " SELECT HumanResources.Department.DepartmentID,HumanResources.Department.Name,HumanResources.Department.GroupName,Person.FirstName,Person.LastName,HumanResources.Employee.JobTitle,HumanResources.Employee.LoginID,Person.EmailAddress.EmailAddress,Person.PersonPhone.PhoneNumber FROM HumanResources.Department INNER JOIN HumanResources.EmployeeDepartmentHistory ON HumanResources.EmployeeDepartmentHistory.DepartmentID = HumanResources.Department.DepartmentID INNER JOIN HumanResources.Employee ON HumanResources.EmployeeDepartmentHistory.BusinessEntityID = HumanResources.Employee.BusinessEntityID INNER JOIN Person.Person ON Person.BusinessEntityID = HumanResources.Employee.BusinessEntityID inner join Person.PersonPhone on Person.PersonPhone.BusinessEntityID = Person.BusinessEntityID inner join Person.EmailAddress on Person.EmailAddress.BusinessEntityID = Person.BusinessEntityID WHERE HumanResources.Employee.OrganizationLevel = 1 AND Person.BusinessEntityID = HumanResources.Employee.BusinessEntityID ORDER BY HumanResources.Department.DepartmentID",
                    conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        List<Department> lists = new List<Department>();
                        while (dr.Read())
                        {
                            Department list = new Department(dr.GetInt16(0), dr.GetString(1), dr.GetString(2),
                                dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetString(7),
                                dr.GetString(8));
                            lists.Add(list);
                        }

                        return lists;
                    }
                }
            }
        }

        public static int GetID()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(" Select max(ID) from Knowledgebase", conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int ID = dr.GetInt32(0);
                            ID++;
                            return ID;
                        }
                        return -1;
                    }
                }
            }
        }

        public static int GetBEID(string userName)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                using (SqlCommand cmd =
                    new SqlCommand(
                        " Select HumanResources.Employee.BusinessEntityID from HumanResources.Employee where HumanResources.Employee.LoginID = @LoginID ",
                        conn))
                {
                    cmd.Parameters.AddWithValue("@LoginID", userName);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int BEID = dr.GetInt32(0);
                            return BEID;
                        }
                    }
                }

                return -1;
            }
        }

        public static object GetImg(string LoginID)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(" select ImgData from [Employee.Photo] inner join HumanResources.Employee on HumanResources.Employee.BusinessEntityID = [Employee.Photo].BusinessEntityID where LoginID = @LoginID", conn))
                {
                    cmd.Parameters.AddWithValue("@LoginID", LoginID);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return dr.GetString(0);
                        }
                        return -1;
                    }
                }
            }
        }

        public static short ValidateDepartment(string userName)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(
                    " SELECT HumanResources.Department.DepartmentID FROM HumanResources.Employee LEFT JOIN HumanResources.EmployeeDepartmentHistory ON HumanResources.Employee.BusinessEntityID = HumanResources.EmployeeDepartmentHistory.BusinessEntityID LEFT JOIN HumanResources.Department ON HumanResources.EmployeeDepartmentHistory.DepartmentID = HumanResources.Department.DepartmentID where HumanResources.EmployeeDepartmentHistory.EndDate IS NULL AND LoginID = @LoginID",
                    conn))
                {
                    cmd.Parameters.AddWithValue("@LoginID", userName);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    {
                        if (dr.Read())
                        {
                            short temp = dr.GetInt16(0);
                            return temp;
                        }
                    }
                }
            }
            return -1;
        }

        public static List<OptionList> GetListOfOption(int departmentID)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                using (SqlCommand cmd =
                    new SqlCommand(" select OptionList.Title from OptionList where OptionList.DepartmentID = @DepartmentID group by Title",
                        conn))
                {
                    cmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    {
                        List<OptionList> lists = new List<OptionList>();
                        while (dr.Read())
                        {
                            OptionList list = new OptionList(dr.GetString(0));
                            lists.Add(list);
                        }
                        return lists;
                    }
                }
            }
        }

        public static List<Chats> GetChats(string userName)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" select * from Room where UserID1 = @BEID or UserID2 = @BEID", conn)) {
                    int BEID = GetBEID(userName);
                    cmd.Parameters.AddWithValue("@BEID", BEID);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        List<Chats> chats = new List<Chats>();
                        while (dr.Read()) {
                            Chats chat = new Chats(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2));
                            chats.Add(chat);
                        }
                        return chats;
                    }
                }
            }
        }

        public static object GetOpenPositions()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(" SELECT * FROM Positions where PositionStatus = 'open';", conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<HR> openPositions = new List<HR>();
                        while (dr.Read())
                        {
                            HR openPosition =new HR(dr.GetInt32(0),dr.GetString(1),dr.GetInt16(2),dr.GetString(3),dr.GetString(4),dr.GetString(5));
                            openPositions.Add(openPosition);
                        }

                        return openPositions;
                    }
                }
            }
        }

        public static object GetCandidate()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(" Select JobCandidateID,Resume from HumanResources.JobCandidate ", conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<HR> candidates = new List<HR>();
                        while (dr.Read())
                        {
                            HR candidate = new HR(dr.GetInt32(0),dr.GetString(1));
                            candidates.Add(candidate);
                        }

                        return candidates;
                    }
                }
            }
        }
        public static object GetCatalog()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" Select Name,ShortDescription from ServiceCatalog", conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        List<Catalog> ServiceCatalogs = new List<Catalog>();
                        while (dr.Read()) {
                            Catalog ServiceCatalog = new Catalog(dr.GetString(0), dr.GetString(1));
                            ServiceCatalogs.Add(ServiceCatalog);
                        }

                        return ServiceCatalogs;
                    }
                }
            }
        }

        public static object getInfo(string loginId)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(" Select FirstName,LastName,PhoneNumber,JobTitle,EmailAddress,Person.businessEntityId from HumanResources.Employee inner join Person.Person on HumanResources.Employee.BusinessEntityID = Person.BusinessEntityID inner join Person.PersonPhone on Person.PersonPhone.BusinessEntityID = Person.BusinessEntityID inner join Person.EmailAddress on Person.EmailAddress.BusinessEntityID = HumanResources.Employee.BusinessEntityID where HumanResources.Employee.LoginID = @loginId", conn))
                {
                    cmd.Parameters.AddWithValue("@loginId", loginId);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Employee> Information = new List<Employee>();
                        while (dr.Read())
                        {
                            Employee info = new Employee(dr.GetString(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4),dr.GetInt32(5));
                            Information.Add(info);
                            
                        }

                        return Information;
                    }
                }
            }
        }
        public static bool updateInfo(string firstName, string lastName,int BusinessEntityID)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(" update Person.Person set FirstName=@firstName , LastName = @lastName  where Person.BusinessEntityID = @BusinessEntityID", conn))
                {
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@BusinessEntityID", BusinessEntityID);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return false;
                        }

                        return true;
                    }
                }
            }
        }

        internal static int GetNewInc()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" select max(ID) from Incidents", conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        if (dr.Read()) {
                            int Inc = dr.GetInt32(0);
                            Inc++;
                            return Inc;
                        } return -1;
                       
                    }
                }
            }
        }
        public static List<DateTime> GetDateTime()
        {
            List<DateTime> dates = new List<DateTime>();
            DateTime date = DateTime.Now.Date;
            date.ToString();
            dates.Add(date);

            return dates;
        }


        public class OptionList
        {
            public object Content { get; set; }

            public OptionList(object Content)
            {
                this.Content = Content;
            }
        }
    }
}