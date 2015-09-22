using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;
namespace DataAccessLayer
{
    public class ConfirmationContext : DataAccessHelper
    {
        private string _tableName = "tblconfirmation";
        private string _pkField = "codeID";
        //public IEnumerable<Confirmation> Fetch { get; set; }
        public IEnumerable<Confirmation> Fetch
        {
            get
            {
                List<Confirmation> codes = new List<Confirmation>();
                using (MySqlConnection myConn = MySqlConn)
                {
                    MySqlCommand cmd = new MySqlCommand
                    {
                        CommandText = QueryBuilder.SelectAll(_tableName),
                        Connection = myConn
                    };
                    myConn.Open();
                    MySqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        Confirmation code = new Confirmation();

                        code.ID = Convert.ToInt32(rd[_pkField]);
                        code.ConfirmationCode = rd["codeNumber"].ToString();                      
                        code.StudentNumber = rd["studentNumber"].ToString();
                        code.Status = Convert.ToBoolean(rd["status"]);
                        codes.Add(code);
                    }

                    myConn.Close();
                }

                return codes;
            }
        }
    }
}
