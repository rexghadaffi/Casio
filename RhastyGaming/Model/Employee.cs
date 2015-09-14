using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public class Employee : UserCredentials
    {
        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Employee Number")]
        public string EmployeeNumber { get; set; }
        [Display(Name = "Department")]
        [Required(ErrorMessage = "This Field is Required")]
        public int DepartmentID { get; set; }
    }

    public class EmployeeList : Employee
    {
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
         [Display(Name = "User Level")]
        public string RoleName { get; set; }
    }

}
