using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserCredentials
    {
        public int ID { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual bool Status { get; set; }
        public virtual int RoleID { get; set; }
        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Middlename { get; set; }
    }
}
