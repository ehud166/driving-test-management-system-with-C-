using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class Manager
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }

        public Manager(string id, string firstName, string password)
        {
            ID = id;
            FirstName = firstName;
            Password = password;
        }
    }
}
