using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;

namespace DataAccessLayer
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
        public string Message
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

            IsValidUser();
            IsActiveUser();
            IsValidCredentials();   
        }

      

        private void IsValidUser()
        {
            int result = 0;

            using (MySqlConnection strConn = MySqlConn)
            {
                string query = string.Format("SELECT COUNT(*) FROM {0} WHERE {1} = ?",
                                               this._tablename,
                                               this._usernameField);
                MySqlCommand cmd = new MySqlCommand(query, strConn);
                cmd.Parameters.AddWithValue("@" + this._usernameField, this._username);

                strConn.Open();

                result = Convert.ToInt32(cmd.ExecuteScalar());

                strConn.Close();
            }

            Message = "";

            if (result <= 0)
            {
                Message = "Your Credentials does not exist in our records.";
            }
        }

        private void IsActiveUser()
        {
            int result = 0;
            using (MySqlConnection strConn = MySqlConn)
            {
                string query = string.Format("SELECT COUNT(*) FROM {0} WHERE {1} = ? AND userStatus = 1",
                                               this._tablename,
                                               this._usernameField);
                MySqlCommand cmd = new MySqlCommand(query, strConn);
                cmd.Parameters.AddWithValue("@" + this._usernameField, this._username);

                strConn.Open();

                result = Convert.ToInt32(cmd.ExecuteScalar());

                strConn.Close();
            }

            if (result <= 0 & Message == "")
            {
                Message = "Your Account is currently disabled. Please contact your Administrator for further details.";
            }
        }

        private void IsValidCredentials()
        {
            int result = 0;
            using (MySqlConnection strConn = MySqlConn)
            {
                string query = string.Format("SELECT * FROM {0} WHERE {1} = ? AND {2} = ?",
                                               this._tablename,
                                               this._usernameField,
                                               this._passwordField);
                MySqlCommand cmd = new MySqlCommand(query, strConn);
                cmd.Parameters.AddWithValue("@" + this._usernameField, this._username);
                cmd.Parameters.AddWithValue("@" + this._passwordField, this._password);

                strConn.Open();

                result = Convert.ToInt32(cmd.ExecuteScalar());

                strConn.Close();
            }

            if (result <= 0 && Message == "")
            {
                Message = "Incorrect Username / Password.";
            }
        }
    }
}
