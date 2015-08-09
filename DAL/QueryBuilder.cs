using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class QueryBuilder
    {
        public static string SelectAll(string TableName)
        {
            return "SELECT * FROM " + TableName;
        }

        public static string SelectWhere(string TableName, string Condition)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}",TableName, Condition);
        }

        public static string Fields(List<string> Fields)
        {
            string result = "";

            foreach (string item in Fields)
	        {
                result += item + ",";
	        }
                        
            return result.Remove(result.Length - 1);
        }

        private static string InsertValues(int count)
        {
            string result = "";
            for (int i = 0; i < count; i++)
            {
                result += "?,";
            }
            return result.Remove(result.Length - 1);          
        }

        public static string Insert(string TableName, List<string> FieldTarget)
        {
            string result = "INSERT INTO " + TableName;

            result = result + string.Format(" ({0}) VALUES({1})",
                                            Fields(FieldTarget),
                                            InsertValues(FieldTarget.Count));
            return result;
        }
    }
}
