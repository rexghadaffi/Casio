using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Accountability
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        [Display(Name = "Department")]
        public int DepartmentID { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        [Display(Name = "Student #")]
        public string StudentNumber { get; set; }
        public bool Status { get; set; }
    }
}
