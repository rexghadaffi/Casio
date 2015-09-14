using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ClearanceViewModel
    {
        public Student Student { get; set; }
        public Dictionary<string, string> Clearance { get; set; }
     
        //public Student Student { get; set; }
        //public Dictionary<Department, string> Departments { get; set; }
        //public string Details { get; set; }
    }
}
