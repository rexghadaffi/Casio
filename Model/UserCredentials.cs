using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class UserCredentials
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool  Status { get; set; }
        public string Roles { get; set; }
    }
}
