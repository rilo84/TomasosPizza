using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.ViewModels
{
    
    public class RoleViewModel
    {
        [Required]
        [DisplayName("RollNamn:")]
        public string Name { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
}
