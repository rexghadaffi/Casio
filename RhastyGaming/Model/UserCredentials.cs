using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserCredentials
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [StringLength(35, MinimumLength = 6, ErrorMessage = "Username has a minimum length of 6 and maximum length of 35")]
        public string Username { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [StringLength(35, MinimumLength = 6, ErrorMessage = "Password has a minimum length of 6 and maximum length of 35")]
        public string Password { get; set; }
        public virtual bool Status { get; set; }
        [Display(Name = "User Level")]
        [Required(ErrorMessage = "This Field is Required")]
        public virtual int RoleID { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public virtual string Firstname { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public virtual string Lastname { get; set; }
        public virtual string Middlename { get; set; }
    }
}
