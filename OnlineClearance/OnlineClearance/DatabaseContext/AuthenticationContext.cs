using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineClearance.Models;

namespace OnlineClearance.DAL
{
    public class AuthenticationContext : DataAccessHelper
    {           
        private string _passwordField;
        private string _usernameField;
        private string _tablename;
        private string _username;
        private string _password;

        public string InitialAction
        {
            get;
            private set;
        }

        public AuthenticationContext(Authentication data)
        {
            this._passwordField = "userName";
            this._usernameField = "userPassword";
            this._tablename = data.Type;                 
            this._username = data.Username;
            this._password = data.Password;          
        }

        public bool IsValidUser()
        {
            int result = 0;

            using (MySqlConnection strConn = MySqlConn)
            {
                string query = string.Format("SELECT COUNT(*) FROM {0} WHERE {1} = ? AND {2} = ?",
                                               this._tablename,
                                               this._usernameField,
                                               this._passwordField);
                MySqlCommand cmd = new MySqlCommand(query, strConn);
                cmd.Parameters.AddWithValue("@" + this._usernameField, this._username);
                cmd.Parameters.AddWithValue("@" + this._passwordField, this._password);

                strConn.Open();

                result = Convert.ToInt32(cmd.ExecuteScalar());

                strConn.Clone();
            }

            return result <= 0 ? false : true;
        }
    }
}
