using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class TransactionContext : DataAccessHelper
    {
        public int _userid;
        private EmployeeContext dbEmployee = new EmployeeContext();
        
        public TransactionContext(int userID)
        {
            _userid = userID;
        }
        public TransactionContext() { }
        public IEnumerable<Audit> GetAllAudit
        {
            get
            {
                List<Audit> audits = new List<Audit>();

                string tables = "tblaudit as a, tblemployee as e";
                string subquery = "a.userID=e.employeeID ORDER BY a.auditID DESC";
                using (MySqlConnection strConn = MySqlConn)
                {
                    strConn.Open();
                    MySqlCommand cmd = new MySqlCommand(QueryBuilder.SelectWhere(tables, subquery),
                                                        strConn);
                    MySqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        Audit audit = new Audit();
                        Employee admin = dbEmployee.GetAllEmployee.FirstOrDefault(a => a.ID == Convert.ToInt32(rd["userID"]));
                        audit.FullName = string.Format("{0}, {1} {2}",
                                                        admin.Lastname,
                                                        admin.Firstname,
                                                        admin.Middlename);
                        audit.ActionDate = Convert.ToDateTime(rd["auditDateTime"]);
                        audit.Action = rd["auditAction"].ToString();
                        audit.Remarks = rd["auditRemarks"].ToString();

                        audits.Add(audit);
                    }
                    strConn.Close();
                }
                return audits;
            }
        }

        public void AddAccountability(string studentNumber)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("@userid", _userid);
            data.Add("@action", "Transaction");
            data.Add("@entrydate", DateTime.Now);
            data.Add("@remarks", "User has added accountability for student #: " + studentNumber);

            SpWithParam("add_audit_trail", data);
        }

        public void MassAccountability()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("@userid", _userid);
            data.Add("@action", "Transaction");
            data.Add("@entrydate", DateTime.Now);
            data.Add("@remarks", "User has uploaded multiple accountabilities");

            SpWithParam("add_audit_trail", data);
        }
    }
}
