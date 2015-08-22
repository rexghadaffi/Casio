using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineClearance.Models
{
    public class Admin
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public bool Status { get; set; }
        [Required(ErrorMessage = "this field is required")]
        [Display(Name = "Role")]
        public int RoleID { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string Lastname { get; set; }
        public string Middlename { get; set; }
    }
}
