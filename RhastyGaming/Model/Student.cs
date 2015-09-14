using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Model
{
    public class Student : UserCredentials
    {
        [Display(Name = "Student #")]
        public string StudentNumber { get; set; }
           [Display(Name = "Course")]
        public string Program { get; set; }
           [Display(Name = "Year Level")]
        public int Year { get; set; }
           [Display(Name = "Semester")]
        public int Term { get; set; }
           [Display(Name = "School Year")]
        public string SchoolYear { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase MassUpload { get; set; }
    }
}
