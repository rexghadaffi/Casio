using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineClearance.DAL
{
    public static class QueryBuilder
    {
        public static string SelectAll(string TableName)
        {
            return "SELECT * FROM " + TableName;
        }

        public static string SelectOrderBy(string TableName, string Key, string Order = "ASC")
        {
            return string.Format("SELECT * FROM {0} {1} {2}", TableName, Key, Order);
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

        public static string Insert(string TableName, List<string> TargetFields)
        {
            string result = "INSERT INTO " + TableName;

            result = result + string.Format(" ({0}) VALUES({1})",
                                            Fields(TargetFields),
                                            InsertValues(TargetFields.Count));
            return result;
        }

        public static string Update(string TableName, List<string> TargetFields, int id, string TargetKey)
        {
            string result = string.Format("UPDATE {0} set {1} WHERE {2} = {3}",
                                          TableName,
                                          SetUpdate(TargetFields),
                                          TargetKey,
                                          id);
            return result;   
        }

        private static string SetUpdate(List<string> TargetFields)
        {
            string result = "";

            foreach (string item in TargetFields)
            {
                result += string.Format("{0} = ?,", item);
            }

            return result.Remove(result.Length - 1);
        }

    }
}
