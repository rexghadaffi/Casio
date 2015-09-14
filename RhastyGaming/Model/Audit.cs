using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Audit
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Action { get; set; }
        public string Remarks { get; set; }
        [Display(Name = "Date & Time")]
        public DateTime ActionDate { get; set; }
        [Display(Name = "Fullname")]
        public string FullName { get; set; }
    }
}
