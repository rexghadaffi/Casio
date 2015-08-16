using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AuditTrail
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Action { get; set; }
        public DateTime DateTime { get; set; }
    }
}
