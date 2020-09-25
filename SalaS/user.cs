using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaS
{
    class User
    {
        public string Uname { get; set; }
        public string Upass { get; set; }
        public User() { }
        public User(string uname, string upass) {
            Uname = uname;
            Upass = upass;
        }
    }
}
