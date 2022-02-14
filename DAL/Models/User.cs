using Microsoft.AspNetCore.Identity;
using System;

namespace DAL
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}
