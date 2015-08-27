using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Student : UserCredentials
    {
        public string StudentNumber { get; set; }
        public string Program { get; set; }
        public int Year { get; set; }
        public int Term { get; set; }
        public string SchoolYear { get; set; }
    }
}
