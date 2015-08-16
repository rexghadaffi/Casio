using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class RolesContext : DataAccessHelper
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

        public IEnumerable<Roles> GetAllRoles
        {
            get
            {
                List<Roles> roles = new List<Roles>();
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
                        Roles role = new Roles();

                        role.ID = Convert.ToInt32(rd[_pkField]);
                        role.Name = rd["name"].ToString();
                        roles.Add(role);
                    }
                    myConn.Close();
                }
                return roles;
            }
        }    
        #endregion

        #region -- Methods --
        public void Insert(Roles department)
        {
            ExecuteNonQuery(QueryBuilder.Insert(_tableName, TargetFields), SetParams(department));
        }

        public void Update(Roles department, int id)
        {
            ExecuteNonQuery(QueryBuilder.Update(_tableName, TargetFields, id, _pkField), SetParams(department));
        }
        // sets parameters for insert/update
        private Dictionary<string, object> SetParams(Roles department)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("@departmentName", department.Name);

            return result;
        }
        #endregion
    }
}
