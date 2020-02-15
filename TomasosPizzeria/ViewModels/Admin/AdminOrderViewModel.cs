using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TomasosPizzeria.Data;
using TomasosPizzeria.Models;

namespace TomasosPizzeria.ViewModels
{
    public class AdminOrderViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public ApplicationUser User { get; set; }
        public List<Bestallning> Orders { get; set; }
        public string UserId { get; set; }
        public int OrderId { get; set; }
        public List<ApplicationUser> Customers { get; set; }

        public UserInfoViewModel UserInfoModel { get; set; }

        public AccountViewModel AccountModel { get; set; }
    }
}
