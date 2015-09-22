using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Confirmation
    {
        public int ID { get; set; }
        public string ConfirmationCode { get; set; }
        public string StudentNumber { get; set; }
        public bool Status { get; set; }
    }
}
