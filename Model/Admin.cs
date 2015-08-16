using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Model
{
    public class Admin 
    {
        public int ID { get; set; }     
        [Required]
        public  string Username { get; set; }
        [Required]
        public  string Password { get; set; }
        [Required]
        public  string Firstname { get; set; }
        [Required]
        public  string Lastname { get; set; }
        [Required]
        public  string Middlename { get; set; }
        public virtual bool Status { get; set; }
        public virtual int RoleID { get; set; }
    }
}
