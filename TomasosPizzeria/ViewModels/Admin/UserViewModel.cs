using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Data;

namespace TomasosPizzeria.ViewModels
{
    public class UserViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
