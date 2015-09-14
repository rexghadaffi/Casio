using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Admin
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [StringLength(35, MinimumLength = 6, ErrorMessage = "Username has a minimum length of 6 and maximum length of 35")]
        public string Username { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [StringLength(35, MinimumLength = 6, ErrorMessage = "Password has a minimum length of 6 and maximum length of 35")]
        public string Password { get; set; }
        public bool Status { get; set; }
        [Display(Name = "User Level")]
        [Required(ErrorMessage = "This Field is Required")]
        public int RoleID { get; set; }
        [Required(ErrorMessage = "This Field is Required")]

        public string Firstname { get; set; }
          [Required(ErrorMessage = "This Field is Required")]
        public string Lastname { get; set; }
        public string Middlename { get; set; }

        public string Role { get; set; }
    }
}
