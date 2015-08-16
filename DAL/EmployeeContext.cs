using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;
namespace DAL
{
    public class EmployeeContext : DataAccessHelper
    {
        #region -- Private Members --
        private static string _pkField = "employeeID";
        private static string _tableName = "tblemployee";
        // related entities
        private static string _tables = "tblemployee as e, tbldepartment as d, tblusertype as u";
        private static string _condition = "d.departmentID = e.departmentID " +
                                           "AND u.userTypeID = e.userTypeID";
        #endregion

        #region -- Properties --
        // maps fields in the database to be inserted or updated.
        private List<string> TargetFields
        {
            get
            {
                return new List<string> { 
                "employeeNumber", "firstName", "lastName", "middleName",
                "departmentID", "userTypeID"
                };
            }
        }
        // fetching data from db
        public IEnumerable<EmployeeList> GetAllEmployee
        {
            get
            {
                List<EmployeeList> employees = new List<EmployeeList>();

                using (MySqlConnection myConn = MySqlConn)
                {
                    MySqlCommand cmd = new MySqlCommand
                    {
                        CommandText = QueryBuilder.SelectWhere(_tables, _condition),
                        Connection = myConn
                    };
                    myConn.Open();
                    MySqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        EmployeeList employee = new EmployeeList();
                        employee.ID = Convert.ToInt32(rd[_pkField]);
                        employee.EmployeeNumber = rd["employeeNumber"].ToString();
                        employee.Firstname = rd["firstName"].ToString();
                        employee.Lastname = rd["lastName"].ToString();
                        employee.Middlename = rd["middleName"].ToString();
                        employee.DepartmentName = rd["departmentName"].ToString();
                        employee.RoleName = rd["userTypename"].ToString();
                        employees.Add(employee);
                    }
                    myConn.Close();
                }

                return employees;
            }

        }
        #endregion

        #region -- Methods --
        public void Insert(Employee employee)
        {
            ExecuteNonQuery(QueryBuilder.Insert(_tableName, TargetFields), SetParams(employee));
        }
        public void Update(Employee employee, int id)
        {
            ExecuteNonQuery(QueryBuilder.Update(_tableName, TargetFields, id, _pkField), SetParams(employee));
        }
        // sets parameters for insert/update
        private Dictionary<string, object> SetParams(Employee employee)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("@employeeNumber", employee.EmployeeNumber);
            result.Add("@firstName", employee.Firstname);
            result.Add("@lastName", employee.Lastname);
            result.Add("@middleName", employee.Middlename);
            result.Add("@departmentID", employee.DepartmentID);
            result.Add("@userTypeID", employee.RoleID);

            return result;
        }
        #endregion 

    }
}
