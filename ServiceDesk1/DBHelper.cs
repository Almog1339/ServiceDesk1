using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
        public static string GetListOfOption(int departmentID)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(" select OptionList.Title from OptionList inner join OptionListSub on OptionListSub.TitleID = OptionList.TitleID where OptionList.DepartmentID = @DepartmentID group by Title",
                        conn))
                {
                    cmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    {
                        if (dr.Read())
                        {
                            string s= "";
                            for (int i = 0; i < dr.FieldCount ; i++)
                            {
                                 s = dr.GetString(i);
                                
                            }

                            return s;

                        }
                    }
                }

                return "";
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
}
