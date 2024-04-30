using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1_Dictionar.Classes
{
    public partial class User
    {
        public string name { get; set; }
        public string password { get; set; }

        public User()
        {
            name = string.Empty;
            password = string.Empty;
        }

        public User(string name, string password)
        {
            this.name = name;
            this.password = password;
        }
    }
}
