using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreIdent.Models.User
{
    public class CreateUserVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Year { get; set; }
    }
}
