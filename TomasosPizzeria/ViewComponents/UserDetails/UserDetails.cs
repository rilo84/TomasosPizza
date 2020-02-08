using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Data;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.ViewComponents.UserDetails
{
    public class UserDetails:ViewComponent
    {
        public IViewComponentResult Invoke(ApplicationUser user)
        {
            return View("UserDetails", user);
        }
    }
}
