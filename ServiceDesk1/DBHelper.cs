using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDesk1
{
    public class DBHelper
    {
        public static string CONN_STRING = "Data Source=DESKTOP-O8IU0PQ\\SQLEXPRESS;Initial Catalog=ServiceDesk;Persist Security Info=True;User ID=sa;Password=Q1w2q1w2";
        public static short ValidateUser(string UserName, string Pass)
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
                        if (dr.Read())
                        {
                           return ValidateDepartment(UserName);
                        }
                    }
                }
                return -1;
            }
        }

        public static short ValidateDepartment(string UserName)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(" SELECT HumanResources.Department.DepartmentID FROM HumanResources.Employee LEFT JOIN HumanResources.EmployeeDepartmentHistory ON HumanResources.Employee.BusinessEntityID = HumanResources.EmployeeDepartmentHistory.BusinessEntityID LEFT JOIN HumanResources.Department ON HumanResources.EmployeeDepartmentHistory.DepartmentID = HumanResources.Department.DepartmentID where HumanResources.EmployeeDepartmentHistory.EndDate IS NULL AND LoginID = @LoginID",conn))
                {
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

        public static object GetNotification(string UserName)
        {
            return -1;
        }

        public static object GetMessages(string UserName)
        {
            return -1;
        }
    }
}
