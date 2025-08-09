using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp
{
    public class User
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public bool VerifyCredentials(string userName, string password)
        {
            return this.userName == userName && this.password == password;
        }

    }
}
