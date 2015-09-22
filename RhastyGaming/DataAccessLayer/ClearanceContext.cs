using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ClearanceContext : DataAccessHelper
    {
        private AccountabilitiesContext dbAccountability = new AccountabilitiesContext();
        private DepartmentContext dbDeparment = new DepartmentContext();
        private StudentContext dbStudent = new StudentContext();

        public List<string> TargetFields 
        {
            get 
            {
                return new List<string> 
                { 
                "codeNumber", "status", "studentNumber"                
                };
            }
        }
        

        public ClearanceViewModel Fetch(string studentNumber)
        {
            ClearanceViewModel vm = new ClearanceViewModel();
            Dictionary<string, string> departmentList = new Dictionary<string, string>();
            List<Department> departments = dbDeparment.GetAllDepartment.ToList();
            List<Accountability> accountabilities =  dbAccountability.Fetch.Where(a => a.StudentNumber == studentNumber && a.Status == true).ToList();

            vm.Student = dbStudent.GetAllStudent.FirstOrDefault(s => s.StudentNumber == studentNumber);



            int count = accountabilities.Count;

            if (count <= 0)
            {

                foreach (Department item in departments)
                {
                    departmentList.Add(item.Name, "Cleared");
                }

                vm.Clearance = departmentList;
            }
            else
            {
                foreach (Department dept in departments)
                {
                    foreach (Accountability acct in accountabilities)
                    {
                        if (dept.ID == acct.DepartmentID)
                        {
                               departmentList.Add(dept.Name, acct.Description);
                        }
                        else
                        {
                            departmentList.Add(dept.Name, "Cleared");
                        }
                    }
                }
                vm.Clearance = departmentList;
            }

            return vm;
        }

        public string RetrieveConfirmationCode(string studentNumber)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("@studentNo", studentNumber);
            string code = StoredProc("get_code", result);
            if (code != "")
            {
                return code;
            }
            else 
            {
                ExecuteNonQuery(QueryBuilder.Insert("tblconfirmation", TargetFields), SetParams(studentNumber));
                return StoredProc("get_code", result);
            }
        }       
      

        private Dictionary<string, object> SetParams(string studentNumber)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("@codeNumber", Guid.NewGuid());
            result.Add("@status", true);
            result.Add("@studentNumber", studentNumber);
            return result;
        }    


    }
}
