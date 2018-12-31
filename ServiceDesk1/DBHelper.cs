using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace ServiceDesk1
{
    public class DBHelper
    {
        private static string CONN_STRING = "Data Source=DESKTOP-O8IU0PQ\\SQLEXPRESS;Initial Catalog=ServiceDesk;Persist Security Info=True;User ID=sa;Password=Q1w2q1w2";
        public static bool ValidateUser(string UserName, string Pass)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT LoginID, PasswordSalt FROM HumanResources.Employee LEFT JOIN PERSON.Password ON HumanResources.Employee.BusinessEntityID = Person.Password.BusinessEntityID WHERE HumanResources.Employee.LoginID = @LoginID AND Person.Password.PasswordSalt = @Password ");
                using (SqlCommand cmd = new SqlCommand(sb.ToString(), conn)) {
                    cmd.Parameters.AddWithValue("@LoginID", UserName);
                    cmd.Parameters.AddWithValue("@Password", Pass);

                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    {
                        if (dr.Read()) {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public static bool PostNewArticle(int ID, string title, string content)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd =
                    new SqlCommand(" Insert into Knowledgebase(ID,Subject,Data) valuse (@ID,@title,@Content)", conn)) {
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@content", content);

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        if (dr.Read()) {
                            return true;
                        }
                    }

                    return false;
                }
            }
        }

        public static List<Employee> UsersList()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand("SELECT Person.BusinessEntityID,Person.FirstName,Person.LastName,HumanResources.Employee.JobTitle,Employee.LoginID,EmailAddress,Person.PhoneNumberType.Name,PhoneNumber FROM Person.Person left join HumanResources.Employee on HumanResources.Employee.BusinessEntityID = Person.BusinessEntityID left join Person.EmailAddress on HumanResources.Employee.BusinessEntityID = Person.EmailAddress.BusinessEntityID left join Person.PersonPhone on Person.PersonPhone.BusinessEntityID = HumanResources.Employee.BusinessEntityID left join Person.PhoneNumberType on Person.PhoneNumberType.PhoneNumberTypeID = Person.PersonPhone.PhoneNumberTypeID WHERE Person.Person.PersonType in ('em', 'sp')", conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        List<Employee> lists = new List<Employee>();
                        while (dr.Read()) {
                            Employee list = new Employee(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetString(7));
                            lists.Add(list);
                        }

                        return lists;
                    }
                }
            }
        }
        public static int GetID()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" Select ID from Knowledgebase", conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        if (dr.Read()) {
                            int ID = dr.GetInt32(0);
                            return ID++;
                        }

                        return -1;
                    }
                }
            }
        }
        public static int GetBEID(string UserName)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" Select HumanResources.Employee.BusinessEntityID from HumanResources.Employee where HumanResources.Employee.LoginID = @LoginID ", conn)) {
                    cmd.Parameters.AddWithValue("@LoginID", UserName);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        if (dr.Read()) {
                            int BEID = dr.GetInt16(0);
                            return BEID;
                        }
                    }
                }

                return -1;
            }
        }

        public static short ValidateDepartment(string UserName)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" SELECT HumanResources.Department.DepartmentID FROM HumanResources.Employee LEFT JOIN HumanResources.EmployeeDepartmentHistory ON HumanResources.Employee.BusinessEntityID = HumanResources.EmployeeDepartmentHistory.BusinessEntityID LEFT JOIN HumanResources.Department ON HumanResources.EmployeeDepartmentHistory.DepartmentID = HumanResources.Department.DepartmentID where HumanResources.EmployeeDepartmentHistory.EndDate IS NULL AND LoginID = @LoginID", conn)) {
                    cmd.Parameters.AddWithValue("@LoginID", UserName);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    {
                        if (dr.Read()) {
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
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" select OptionList.Title from OptionList where OptionList.DepartmentID = @DepartmentID group by Title", conn)) {
                    cmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    {
                        List<OptionList> lists = new List<OptionList>();
                        while (dr.Read()) {
                            OptionList list = new OptionList(dr.GetString(0));
                            lists.Add(list);
                        }

                        return lists;
                    }
                }
            }
        }

        public static object GetNotification(string UserName)
        {
            return -1;
        }

        public static object GetMessages(string UserName)
        {
            return -1;
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