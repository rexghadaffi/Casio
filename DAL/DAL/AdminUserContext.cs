using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;
namespace DAL
{
    public class AdminUserContext : DataAccessHelper
    {
        #region -- Private Members --
        private static string _pkField = "userID";
        private static string _tableName = "tblcompanyuser";
        private static string _tables = "tblcompanyuser as c, tblusertype as u";
        private static string _condition = "c.userTypeID = u.userTypeID";
        #endregion

        #region -- Properties --
        // maps fields in the database to be inserted or updated.
        private List<string> TargetFields
        {
            get
            {
                return new List<string> { "userName", "userPassword", "userStatus", "userTypeID" };
            }
        }

        public IEnumerable<Admin> GetAllDepartment
        {
            get
            {
                List<Admin> admins = new List<Admin>();
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
                        Admin admin = new Admin();

                        admin.ID = Convert.ToInt32(rd[_pkField]);
                        admin.Username = rd["userName"].ToString();
                        admin.Password = rd["userPassword"].ToString();
                        admin.Status = Convert.ToBoolean(rd["userStatus"]);
                        admin.RoleID = Convert.ToInt32(rd["userTypeID"]);
                        admins.Add(admin);
                    }

                    myConn.Close();
                }

                return admins;
            }
        }
        #endregion

        #region -- Methods --
        public void Insert(Admin department)
        {
            ExecuteNonQuery(QueryBuilder.Insert(_tableName, TargetFields), SetParams(department));
        }

        public void Update(Admin department, int id)
        {
            ExecuteNonQuery(QueryBuilder.Update(_tableName, TargetFields, id, _pkField), SetParams(department));
        }
        // sets parameters for insert/update

        private Dictionary<string, object> SetParams(Admin department)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("@userName", department.Username);
            result.Add("@userPassword", department.Password);
            result.Add("@userStatus", department.Status);
            result.Add("@userTypeID", department.RoleID);
            return result;
        }
        #endregion

     

    }
}
