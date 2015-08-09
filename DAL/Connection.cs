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
        private static string _connString = "conn";

        public static MySqlConnection GetSqlConnection
        {
            get {
                return new MySqlConnection(SetConnection());
            }
        }

        private static string SetConnection()
        {
            return ConfigurationManager.ConnectionStrings[_connString].ConnectionString;
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

