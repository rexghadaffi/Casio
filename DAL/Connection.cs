using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace DAL
{
    public static class Connection
    {
       
        public static MySqlConnection GetSqlConnection
        {
            get
            {
                return new MySqlConnection(ConnectionString);
            }
        }

        private static string ConnectionString
        {
            get
            {
                return "Data Source=127.0.0.1;Initial Catalog=db_clearance;User Id=root;Password=;Charset=utf8";
            }
        }

        public static string SetConnection(string ConnectionString)
        {
            return ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString;
        }

        public static MySqlConnection GetConnection(string ConnectionString)
        {
            return new MySqlConnection(ConnectionString);
        }

        public static void Dispose()
        {
            Dispose();
        }
    }
}

