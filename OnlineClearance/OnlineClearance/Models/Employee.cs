using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineClearance.Models
{
    public class Employee : UserCredentials
    {       
        public string EmployeeNumber { get; set; }
        public int DepartmentID { get; set; }                
    }

    public class EmployeeList : Employee
    {
        public string DepartmentName { get; set; }
        public string RoleName { get; set; }
    }
    
}
