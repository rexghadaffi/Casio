using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManualTest
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeContext dbEmployee = new EmployeeContext();
            Employee newEmployee = new Employee { 
            EmployeeNumber = "005-2011-0311",
            Firstname = "feye",
            Lastname = "ebol",
            Middlename = "ferrer",
            DepartmentID = 1,
            RoleID = 2
            };
           
                PrintEmployee(dbEmployee.GetAllEmployee.ToList());
          
            Console.ReadLine();
        }
        public static void PrintEmployee(IEnumerable<Employee> list)
        {
            string result = "";
            foreach (Employee item in list)
            {
                result = string.Format("ID : {0} Number: {1} Firstname: {2}",
                    item.ID,
                    item.EmployeeNumber,
                    item.Firstname);
                Console.WriteLine(result);
            }
        }
    }
}
