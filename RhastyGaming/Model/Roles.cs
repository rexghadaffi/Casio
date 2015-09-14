using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Roles
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This Field is Required.")]
        public string Name { get; set; }
    }
}
