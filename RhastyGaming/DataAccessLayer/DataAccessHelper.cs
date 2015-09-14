using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataAccessLayer
{
    public abstract class DataAccessHelper
    {
        protected MySqlConnection MySqlConn
        {
            get
            {
                return Connection.GetSqlConnection;
            }
        }

        protected virtual void SpWithParam(string paramName, Dictionary<string, object> data)
        {
            using (MySqlConnection strConn = MySqlConn)
            {
                MySqlCommand cmd = new MySqlCommand(paramName, strConn);
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, object> value in data)
                {
                    cmd.Parameters.AddWithValue(value.Key, value.Value);
                }

                strConn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        protected virtual void SpWithID(string paramName, int id)
        {
            using (MySqlConnection strConn = MySqlConn)
            {
                MySqlCommand cmd = new MySqlCommand(paramName, strConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);

                strConn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        protected virtual int StoredProc(string paramName)
        {
            int result = 0;
            using (MySqlConnection strConn = MySqlConn)
            {
                MySqlCommand cmd = new MySqlCommand(paramName, strConn);
                cmd.CommandType = CommandType.StoredProcedure;

                strConn.Open();
                return result = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        protected virtual int GetUserID(string username, string fieldname)
        {
            int result = 0;
            using (MySqlConnection strConn = MySqlConn)
            {
                MySqlCommand cmd = new MySqlCommand("get_user_id", strConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);

                strConn.Open();
                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    result = Convert.ToInt32(rd[fieldname]);
                }
            }
            return result;
        }

        protected virtual MySqlCommand SetParameters(MySqlCommand cmd, Dictionary<string, object> parameters)
        {
            foreach (KeyValuePair<string, object> value in parameters)
            {
                cmd.Parameters.AddWithValue(value.Key, value.Value);
            }

            return cmd;
        }

        protected virtual MySqlCommand SetMySqlCommand(MySqlConnection conn, string query)
        {
            return new MySqlCommand(query, conn);
        }

        protected virtual void ExecuteNonQuery(string query)
        {
            using (MySqlConnection strConn = MySqlConn)
            {
                strConn.Open();
                MySqlCommand cmd = new MySqlCommand(query, strConn);
                cmd.ExecuteNonQuery();
                strConn.Close();
            }
        }

        protected virtual void ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            using (MySqlConnection strConn = MySqlConn)
            {
                strConn.Open();
                MySqlCommand cmd = new MySqlCommand(query, strConn);
                SetParameters(cmd, parameters);
                cmd.ExecuteNonQuery();
                strConn.Close();
            }
        }

        protected virtual object GetFieldID(string tablename, string field,string key, object value)
        {
            string query = string.Format("SELECT {0} FROM {1} WHERE {2} = '{3}'",
                                         field, tablename, key, value);
            using (MySqlConnection strConn = MySqlConn)
            {
                strConn.Open();
                MySqlCommand cmd = new MySqlCommand(query, strConn);
                MySqlDataReader rd = cmd.ExecuteReader();
                object result = null;
                while (rd.Read())
                {
                     result =  rd[field];
                }
                strConn.Close();

                return result;
            }
        }
    }
}