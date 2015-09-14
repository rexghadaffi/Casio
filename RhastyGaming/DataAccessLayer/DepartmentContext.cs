using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;

namespace DataAccessLayer
{
    public class DepartmentContext : DataAccessHelper
    {
        #region -- Private Members --
        private static string _pkField = "departmentID";
        private static string _tableName = "tbldepartment";
        
        #endregion

        #region -- Properties --
        // maps fields in the database to be inserted or updated.
        private List<string> TargetFields
        {  
            get
            {
                return new List<string> { "departmentName" };
            }
        }
        public IEnumerable<Department> GetAllDepartment
        {
            get
            {
                List<Department> departments = new List<Department>();
                using (MySqlConnection myConn = MySqlConn)
                {
                    MySqlCommand cmd = new MySqlCommand
                    {
                        CommandText = QueryBuilder.SelectAll(_tableName),
                        Connection = myConn
                    };
                    myConn.Open();
                    MySqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        Department department = new Department();

                        department.ID = Convert.ToInt32(rd[_pkField]);
                        department.Name = rd["departmentName"].ToString();
                        departments.Add(department);
                    }
                    
                    myConn.Close();
                }

                return departments;
            }
        }
        #endregion

        #region -- Methods --
        public void Insert(Department department)
        {
            ExecuteNonQuery(QueryBuilder.Insert(_tableName, TargetFields), SetParams(department));
        }

        public void Update(Department department, int id)
        {
            ExecuteNonQuery(QueryBuilder.Update(_tableName, TargetFields, id, _pkField), SetParams(department));
        }
        // sets parameters for insert/update
        private Dictionary<string, object> SetParams(Department department)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("@departmentName", department.Name);         

            return result;
        }
        #endregion    


    }
}
