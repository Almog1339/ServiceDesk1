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
                using (SqlCommand cmd = new SqlCommand(" select ID,open_for,Short_Description,Description,GroupName,State,Category,Impact,Urgency,Assigned_to from Incidents where Assigned_to = ' '", conn)) {
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

        internal static bool ChangeTheme(string Color,string LoginID)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand("update HumanResources.Employee set Theme = @color where Employee.LoginID = @LoginID")) {
                    cmd.Parameters.AddWithValue("@color", Color);
                    cmd.Parameters.AddWithValue("@LoginID", LoginID);
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

        internal static object SubmitNewTicket(string LoginId, string shortDescription, string description, string category)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" insert into incidents (open_for,Short_Description,Description,Category,GroupName,state,impact,urgency) values (@loginId,@shortDescription,@description,@Category,'Global Service Desk','Open',3,3)", conn)) {
                    cmd.Parameters.AddWithValue("@loginId", LoginId);
                    cmd.Parameters.AddWithValue("@shortDescription", shortDescription);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@Category", category);
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

        internal static object ResolveInc()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" Select ID,open_for,Short_Description,Description,GroupName,State,Category,Impact,Urgency from Incidents where State = 'Resolve'", conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        List<Tickets> tickets = new List<Tickets>();
                        while (dr.Read()) {
                            Tickets ticket = new Tickets(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetInt16(7), dr.GetInt16(8));
                            tickets.Add(ticket);

                        }
                        return tickets;
                    }
                }
            }
        }

        internal static object AssignToMe(string username)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" Select ID,open_for,Short_Description,Description,GroupName,State,Category,Impact,Urgency,Assigned_to from Incidents where Assigned_to = @userName", conn)) {
                    cmd.Parameters.AddWithValue("@userName", username);
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

        internal static object GetOpenTicket()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" select ID,open_for,Short_Description,Description,GroupName,State,Category,Impact,Urgency from Incidents where State = 'Open'", conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        List<Tickets> tickets = new List<Tickets>();
                        while (dr.Read()) {
                            Tickets ticket = new Tickets(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetInt16(7), dr.GetInt16(8));
                            tickets.Add(ticket);

                        }
                        return tickets;
                    }

                }
            }
        }

        internal static List<Global> GetStores(string state)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" select State,Address,ZipCode,Phone from States inner join Stores on Stores.StateID = States.ID where State = @state", conn)) {
                    cmd.Parameters.AddWithValue("@state", state);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        List<Global> Stores = new List<Global>();
                        while (dr.Read()) {
                            Global store = new Global(dr.GetString(0), dr.GetString(1), dr.GetString(2), dr.GetString(3));
                            Stores.Add(store);

                        }
                        return Stores;
                    }
                }
            }
        }

        public static int PositionID()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" select max(JobID) from Positions", conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
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
                using (SqlCommand cmd = new SqlCommand("Select ImgData from Images", conn)) {
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
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                StringBuilder sb = new StringBuilder();
                sb.Append(
                    "SELECT LoginID, PasswordSalt FROM HumanResources.Employee LEFT JOIN PERSON.Password ON HumanResources.Employee.BusinessEntityID = Person.Password.BusinessEntityID WHERE HumanResources.Employee.LoginID = @LoginID AND Person.Password.PasswordSalt = @Password ");
                using (SqlCommand cmd = new SqlCommand(sb.ToString(), conn)) {
                    cmd.Parameters.AddWithValue("@LoginID", userName);
                    cmd.Parameters.AddWithValue("@Password", pass);

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

        public static bool passwordReset(string loginID, string password, string newPassword, int BusinessEntityID)
        {
            if (ValidateUser(loginID, password) == true) {
                using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                    using (SqlCommand cmd =
                        new SqlCommand(
                            " update Person.Password set PasswordSalt = '@newPassword' where BusinessEntityID = @BusinessEntityID",
                            conn)) {
                        cmd.Parameters.AddWithValue("@newPassword", newPassword);
                        cmd.Parameters.AddWithValue("@BusinessEntityID", BusinessEntityID);
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
            else {
                return false;
            }
        }

        public static bool PostNewArticle(string Subject, string content, int businessEntityID, string postedByName, string title)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd =
                    new SqlCommand(
                        " Insert into Knowledgebase(Subject,Data,PostedBy,PostedByLoginID,Title) values(@Subject,@Content,@BusinessEntityID,@PostedByLoginID)",
                        conn)) {
                    cmd.Parameters.AddWithValue("@Subject", Subject);
                    cmd.Parameters.AddWithValue("@content", content);
                    cmd.Parameters.AddWithValue("@BusinessEntityID", businessEntityID);
                    cmd.Parameters.AddWithValue("@PostedByLoginID", postedByName);
                    cmd.Parameters.AddWithValue("@Title", title);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        if (dr.Read()) {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }
        public static List<Employee> UsersListWithoutPic()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(
                    " SELECT Person.BusinessEntityID,Person.FirstName, Person.LastName, HumanResources.Employee.JobTitle, Employee.LoginID, EmailAddress, Person.PhoneNumberType.Name, PhoneNumber FROM Person.Person left join HumanResources.Employee on HumanResources.Employee.BusinessEntityID = Person.BusinessEntityID left join Person.EmailAddress on HumanResources.Employee.BusinessEntityID = Person.EmailAddress.BusinessEntityID left join Person.PersonPhone on Person.PersonPhone.BusinessEntityID = HumanResources.Employee.BusinessEntityID left join Person.PhoneNumberType on Person.PhoneNumberType.PhoneNumberTypeID = Person.PersonPhone.PhoneNumberTypeID WHERE Person.Person.PersonType in ('em', 'sp')",
                    conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {

                        List<Employee> lists = new List<Employee>();
                        while (dr.Read()) {
                            Employee list = new Employee(dr.GetInt32(0), dr.GetString(1), dr.GetString(2),
                                dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetString(7));
                            lists.Add(list);
                        }

                        return lists;
                    }
                }
            }
        }
        public static List<Employee> UsersList()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(
                    " SELECT Person.BusinessEntityID,Person.FirstName, Person.LastName, HumanResources.Employee.JobTitle, Employee.LoginID, EmailAddress, Person.PhoneNumberType.Name, PhoneNumber,[Employee.Photo].ImgData FROM Person.Person left join HumanResources.Employee on HumanResources.Employee.BusinessEntityID = Person.BusinessEntityID left join Person.EmailAddress on HumanResources.Employee.BusinessEntityID = Person.EmailAddress.BusinessEntityID left join Person.PersonPhone on Person.PersonPhone.BusinessEntityID = HumanResources.Employee.BusinessEntityID left join Person.PhoneNumberType on Person.PhoneNumberType.PhoneNumberTypeID = Person.PersonPhone.PhoneNumberTypeID left join[Employee.Photo] on[Employee.Photo].BusinessEntityID = Person.BusinessEntityID WHERE Person.Person.PersonType in ('em', 'sp')",
                    conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {

                        List<Employee> lists = new List<Employee>();
                        while (dr.Read()) {
                            Employee list = new Employee(dr.GetInt32(0), dr.GetString(1), dr.GetString(2),
                                dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetString(7), dr.GetString(8));
                            lists.Add(list);
                        }

                        return lists;
                    }
                }
            }
        }

        public static List<Knowledge> GetArticles(string Subject)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" select * from Knowledgebase where Subject = @Subject",
                    conn)) {
                    cmd.Parameters.AddWithValue("@Subject", Subject);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        List<Knowledge> lists = new List<Knowledge>();
                        while (dr.Read()) {
                            Knowledge item = new Knowledge(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetInt32(3), dr.GetString(4),dr.GetString(5));
                            lists.Add(item);
                        }
                        return lists;
                    }
                }
            }
        }
        public static object GetSubjects()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand("select subject from Knowledgebase group by Subject", conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        List<Knowledge> subjects = new List<Knowledge>();
                        while (dr.Read()) {
                            Knowledge sub = new Knowledge(dr.GetString(0));
                            subjects.Add(sub);
                        }
                        return subjects;
                    }
                }
            }
        }
        public static List<Department> DepartmentList()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(
                    " SELECT HumanResources.Department.DepartmentID,HumanResources.Department.Name,HumanResources.Department.GroupName,Person.FirstName,Person.LastName,HumanResources.Employee.JobTitle,HumanResources.Employee.LoginID,Person.EmailAddress.EmailAddress,Person.PersonPhone.PhoneNumber FROM HumanResources.Department INNER JOIN HumanResources.EmployeeDepartmentHistory ON HumanResources.EmployeeDepartmentHistory.DepartmentID = HumanResources.Department.DepartmentID INNER JOIN HumanResources.Employee ON HumanResources.EmployeeDepartmentHistory.BusinessEntityID = HumanResources.Employee.BusinessEntityID INNER JOIN Person.Person ON Person.BusinessEntityID = HumanResources.Employee.BusinessEntityID inner join Person.PersonPhone on Person.PersonPhone.BusinessEntityID = Person.BusinessEntityID inner join Person.EmailAddress on Person.EmailAddress.BusinessEntityID = Person.BusinessEntityID WHERE HumanResources.Employee.OrganizationLevel = 1 AND Person.BusinessEntityID = HumanResources.Employee.BusinessEntityID ORDER BY HumanResources.Department.DepartmentID",
                    conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {

                        List<Department> lists = new List<Department>();
                        while (dr.Read()) {
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
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" Select max(ID) from Knowledgebase", conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        if (dr.Read()) {
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
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd =
                    new SqlCommand(
                        " Select HumanResources.Employee.BusinessEntityID from HumanResources.Employee where HumanResources.Employee.LoginID = @LoginID ",
                        conn)) {
                    cmd.Parameters.AddWithValue("@LoginID", userName);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        if (dr.Read()) {
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
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" select ImgData from [Employee.Photo] inner join HumanResources.Employee on HumanResources.Employee.BusinessEntityID = [Employee.Photo].BusinessEntityID where LoginID = @LoginID", conn)) {
                    cmd.Parameters.AddWithValue("@LoginID", LoginID);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        if (dr.Read()) {
                            return dr.GetString(0);
                        }
                        return -1;
                    }
                }
            }
        }

        public static short ValidateDepartment(string userName)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(
                    " SELECT HumanResources.Department.DepartmentID FROM HumanResources.Employee LEFT JOIN HumanResources.EmployeeDepartmentHistory ON HumanResources.Employee.BusinessEntityID = HumanResources.EmployeeDepartmentHistory.BusinessEntityID LEFT JOIN HumanResources.Department ON HumanResources.EmployeeDepartmentHistory.DepartmentID = HumanResources.Department.DepartmentID where HumanResources.EmployeeDepartmentHistory.EndDate IS NULL AND LoginID = @LoginID",
                    conn)) {
                    cmd.Parameters.AddWithValue("@LoginID", userName);
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
                using (SqlCommand cmd =
                    new SqlCommand(" select OptionList.Title from OptionList where OptionList.DepartmentID = @DepartmentID group by Title",
                        conn)) {
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

        //public static List<Chats> GetChats(string userName)
        //{
        //    using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
        //        using (SqlCommand cmd = new SqlCommand(" select * from Room where UserID1 = @BEID or UserID2 = @BEID", conn)) {
        //            int BEID = GetBEID(userName);
        //            cmd.Parameters.AddWithValue("@BEID", BEID);
        //            conn.Open();
        //            using (SqlDataReader dr = cmd.ExecuteReader()) {
        //                List<Chats> chats = new List<Chats>();
        //                while (dr.Read()) {
        //                    Chats chat = new Chats(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2));
        //                    chats.Add(chat);
        //                }
        //                return chats;
        //            }
        //        }
        //    }
        //}

        public static object GetOpenPositions()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" SELECT * FROM Positions where PositionStatus = 'open';", conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        List<HR> openPositions = new List<HR>();
                        while (dr.Read()) {
                            HR openPosition = new HR(dr.GetInt32(0), dr.GetString(1), dr.GetInt16(2), dr.GetString(3), dr.GetString(4), dr.GetString(5));
                            openPositions.Add(openPosition);
                        }

                        return openPositions;
                    }
                }
            }
        }

        public static object GetCandidate()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" Select JobCandidateID,Resume from HumanResources.JobCandidate ", conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        List<HR> candidates = new List<HR>();
                        while (dr.Read()) {
                            HR candidate = new HR(dr.GetInt32(0), dr.GetString(1));
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
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" Select FirstName,LastName,PhoneNumber,JobTitle,EmailAddress,Person.businessEntityId from HumanResources.Employee inner join Person.Person on HumanResources.Employee.BusinessEntityID = Person.BusinessEntityID inner join Person.PersonPhone on Person.PersonPhone.BusinessEntityID = Person.BusinessEntityID inner join Person.EmailAddress on Person.EmailAddress.BusinessEntityID = HumanResources.Employee.BusinessEntityID where HumanResources.Employee.LoginID = @loginId", conn)) {
                    cmd.Parameters.AddWithValue("@loginId", loginId);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        List<Employee> Information = new List<Employee>();
                        while (dr.Read()) {
                            Employee info = new Employee(dr.GetString(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetInt32(5));
                            Information.Add(info);

                        }

                        return Information;
                    }
                }
            }
        }
        public static bool updateInfo(string firstName, string lastName, int BusinessEntityID)
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" update Person.Person set FirstName=@firstName , LastName = @lastName  where Person.BusinessEntityID = @BusinessEntityID", conn)) {
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@BusinessEntityID", BusinessEntityID);
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
                        }
                        return -1;

                    }
                }
            }
        }
        public static object GetDateTime()
        {
            List<DateTime> dates = new List<DateTime>();
            DateTime date = DateTime.Now.Date;
            date.ToString();
            dates.Add(date);

            return dates;
        }

        internal static object GetSuppliers()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" select SupplierID,CompanyName,ContactName,ContactTitle,Address,City,PostalCode,Country,Phone,Fax,HomePage from Suppliers", conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        List<Inventory> suppliers = new List<Inventory>();
                        while (dr.Read()) {
                            Inventory supplier = new Inventory(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetString(7), dr.GetString(8), dr.GetString(9), dr.GetString(10));
                            suppliers.Add(supplier);
                        }
                        return suppliers;
                    }
                }
            }
        }
        internal static object GetOrder()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" select OrderID,EmployeeID,FirstName,LastName,OrderDate,RequiredDate,ShippedDate,Freight,ShipName,ShipAddress,ShipCity,ShipCountry from Orders left join Person.Person on Person.BusinessEntityID = Orders.EmployeeID ", conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        List<Inventory> orders = new List<Inventory>();
                        while (dr.Read()) {
                            Inventory order = new Inventory(dr.GetInt32(0), dr.GetInt32(1), dr.GetString(2), dr.GetString(3), dr.GetDateTime(4), dr.GetDateTime(5), dr.GetDateTime(6), dr.GetInt32(7), dr.GetString(8), dr.GetString(9), dr.GetString(10), dr.GetString(11));
                            orders.Add(order);
                        }
                        return orders;
                    }
                }
            }
        }

        internal static object NewOrder(string LoginID, DateTime orderDate, DateTime required_Date, DateTime shipped_Date, int freight, string shipName, string shipAddress, string shipCity, string shipCountry)
        {
            int businessEntityID = GetBEID(LoginID);
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {

                using (SqlCommand cmd = new SqlCommand(" insert into Orders(EmployeeID,OrderDate,RequiredDate,ShippedDate,Freight,ShipName,ShipAddress,ShipCity,ShipCountry) values (@businessEntityID,@orderDate,@required_Date,@shipped_Date,@freight,@shipName,@shipAddress,@shipCity,@shipCountry)", conn)) {
                    cmd.Parameters.AddWithValue("@businessEntityID", businessEntityID);
                    cmd.Parameters.AddWithValue("@orderDate", orderDate);
                    cmd.Parameters.AddWithValue("@required_Date", required_Date);
                    cmd.Parameters.AddWithValue("@shipped_Date", shipped_Date);
                    cmd.Parameters.AddWithValue("@freight", freight);
                    cmd.Parameters.AddWithValue("@shipName", shipName);
                    cmd.Parameters.AddWithValue("@shipAddress", shipAddress);
                    cmd.Parameters.AddWithValue("@shipCity", shipCity);
                    cmd.Parameters.AddWithValue("@shipCountry", shipCountry);
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

        internal static object GetInvocies()
        {
            using (SqlConnection conn = new SqlConnection(CONN_STRING)) {
                using (SqlCommand cmd = new SqlCommand(" select OrderID,ShipName,ShipAddress,ShipCity,ShipCountry,CustomerName,OrderDate,ShippedDate,RequiredDate,ProductID,ProductName,UnitPrice,Quantity,Discount,ExtendedPrice,Freight from Invoices", conn)) {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        List<Finance> invoices = new List<Finance>();
                        while (dr.Read()) {
                            Finance invoice = new Finance(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetDateTime(6), dr.GetDateTime(7), dr.GetDateTime(8), dr.GetInt32(9),dr.GetString(10), dr.GetInt32(11), dr.GetInt16(12), dr.GetInt32(13), dr.GetInt32(14), dr.GetInt32(15));
                            invoices.Add(invoice);
                        }
                        return invoices;
                    }
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
