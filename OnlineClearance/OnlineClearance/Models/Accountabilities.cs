using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineClearance.Models
{
    public class Accountabilities
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string StudentNumber { get; set; }
        public bool Status { get; set; }
        public int DepartmentID { get; set; }
        public Department Department { get; set; }
    }
}
