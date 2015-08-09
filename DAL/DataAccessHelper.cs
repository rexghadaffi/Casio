using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace DAL
{
    public abstract class DataAccessHelper 
    {
        public MySqlConnection MySqlConn {  get {
            return Connection.GetSqlConnection; }                    
        }

        public virtual void SpWithParam(string paramName, Dictionary<string, object> data)
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

        public virtual void SpWithID(string paramName, int id)
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

        public virtual int StoredProc(string paramName)
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
        
        public virtual int GetUserID(string username, string fieldName)
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
                    result = Convert.ToInt32(rd[fieldName]);
                }
            }
            return result;
        }
    }
}