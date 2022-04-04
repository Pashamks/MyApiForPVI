using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab5.Models
{
    public class LoginModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool StayAnonim { get; set; }
        public string Level { get; set; }
    }
}
