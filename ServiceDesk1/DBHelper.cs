using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
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

        public static bool PostNewArticle(string title, string content, int businessEntityID, string postedByName)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                using (SqlCommand cmd =
                    new SqlCommand(
                        " Insert into Knowledgebase(Subject,Data,PostedBy,PostedByLoginID) values(@title,@Content,@BusinessEntityID,@PostedByLoginID)",
                        conn))
                {
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@content", content);
                    cmd.Parameters.AddWithValue("@BusinessEntityID", businessEntityID);
                    cmd.Parameters.AddWithValue("@PostedByLoginID", postedByName);
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
                    " SELECT Person.BusinessEntityID,Person.FirstName,Person.LastName,HumanResources.Employee.JobTitle,Employee.LoginID,EmailAddress,Person.PhoneNumberType.Name,PhoneNumber FROM Person.Person left join HumanResources.Employee on HumanResources.Employee.BusinessEntityID = Person.BusinessEntityID left join Person.EmailAddress on HumanResources.Employee.BusinessEntityID = Person.EmailAddress.BusinessEntityID left join Person.PersonPhone on Person.PersonPhone.BusinessEntityID = HumanResources.Employee.BusinessEntityID left join Person.PhoneNumberType on Person.PhoneNumberType.PhoneNumberTypeID = Person.PersonPhone.PhoneNumberTypeID WHERE Person.Person.PersonType in ('em', 'sp')",
                    conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        List<Employee> lists = new List<Employee>();
                        while (dr.Read())
                        {
                            Employee list = new Employee(dr.GetInt32(0), dr.GetString(1), dr.GetString(2),
                                dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetString(7));
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
                using (SqlCommand cmd = new SqlCommand(" select ID,Subject,Data,PostedByLoginID from Knowledgebase",
                    conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Knowledge> lists = new List<Knowledge>();
                        while (dr.Read())
                        {
                            Knowledge item = new Knowledge(dr.GetInt32(0), dr.GetString(1), dr.GetString(2),
                                dr.GetString(3));
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

        public static object GetImg(string userName)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                using (SqlCommand cmd =
                    new SqlCommand(
                        " select Photo from [Employess.Photo] inner join HumanResources.Employee on HumanResources.Employee.BusinessEntityID = [Employess.Photo].BusinessEntityID where LoginID = @LoginID",
                        conn))
                {
                    cmd.Parameters.AddWithValue("@LoginID", userName);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return dr.GetSqlBinary(0);
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

        public static object GetChats(string UserName)
        {
            return -1;
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