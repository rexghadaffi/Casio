using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using OnlineClearance.Models;


namespace OnlineClearance.DAL
{
    public class AdminContext : DataAccessHelper
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
                return new List<string> { "userName",
                                          "userPassword",
                                          "userStatus",
                                          "userTypeID",
                                          "firstName",
                                          "lastName",
                                          "middleName"};
            }
        }

        public IEnumerable<Admin> GetAllAdmin
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
                        admin.Firstname = rd["firstName"].ToString();
                        admin.Lastname = rd["lastName"].ToString();
                        admin.Middlename = rd["middleName"].ToString();
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

        private Dictionary<string, object> SetParams(Admin admin)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("@userName", admin.Username);
            result.Add("@userPassword", admin.Password);
            result.Add("@userStatus", admin.Status);
            result.Add("@userTypeID", admin.RoleID);
            result.Add("@firstName", admin.Firstname);
            result.Add("@lastName", admin.Lastname);
            result.Add("@middleName", admin.Middlename);

            return result;
        }
        #endregion

    }
}
