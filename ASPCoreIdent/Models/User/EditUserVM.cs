using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreIdent.Models.User
{
    public class EditUserVM
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public int Year { get; set; }
        public int Phone { get; set; }
    }
}
