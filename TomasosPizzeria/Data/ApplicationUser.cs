using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string PostalNumber { get; set; }
        public string City { get; set; }
        public int BonusPoints { get; set; }
    }
}
