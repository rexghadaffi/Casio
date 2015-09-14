using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccessLayer
{
    public class AuditContext : DataAccessHelper
    {
        public int _userid;
        private AdminContext dbAdmin = new AdminContext();
        
        public AuditContext(int userID)
        {
            _userid = userID;
        }
        public AuditContext() { }
        public IEnumerable<Audit> GetAllAudit
        {
            get
            {
                List<Audit> audits = new List<Audit>();

                string tables = "tblaudit as a, tblcompanyuser as c";
                string subquery = "a.userID=c.userID ORDER BY a.auditID DESC";
                using (MySqlConnection strConn = MySqlConn)
                {
                    strConn.Open();
                    MySqlCommand cmd = new MySqlCommand(QueryBuilder.SelectWhere(tables, subquery),
                                                        strConn);
                    MySqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        Audit audit = new Audit();
                        Admin admin = dbAdmin.GetAllAdmin.FirstOrDefault(a => a.ID == Convert.ToInt32(rd["userID"]));
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

        public void Add(string remarks)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("@userid", _userid);
            data.Add("@action", "Created A New Record");
            data.Add("@entrydate", DateTime.Now);
            data.Add("@remarks", remarks);

            SpWithParam("add_audit_trail", data);
        }

        public void Edit(string remarks)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("@userid", _userid);
            data.Add("@action", "Modified A Record");
            data.Add("@entrydate", DateTime.Now);
            data.Add("@remarks", remarks);

            SpWithParam("add_audit_trail", data);
        }

        public void LoggedIn()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("@userid", _userid);
            data.Add("@action", "Logged In");
            data.Add("@entrydate", DateTime.Now);
            data.Add("@remarks", "User has Logged in the STI Clearance System.");

            SpWithParam("add_audit_trail", data);
        }

        public void LoggedOut()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("@userid", _userid);
            data.Add("@action", "Logged In");
            data.Add("@entrydate", DateTime.Now);
            data.Add("@remarks", "User has Logged Out of the STI Clearance System.");

            SpWithParam("add_audit_trail", data);
        }       
    }
}
