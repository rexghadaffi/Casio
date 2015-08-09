using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Admin : UserCredentials
    {
        public string Fullname { get; set; }
        public int DepartmentID { get; set; }
        public Department Department { get; set; }
    }
}
