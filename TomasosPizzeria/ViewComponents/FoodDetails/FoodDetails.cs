using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.ViewComponents.FoodDetails
{
    public class FoodDetails : ViewComponent
    {
        public IViewComponentResult Invoke(FoodManageViewModel model)
        {
            return View("FoodDetails", model);
        }
    }
}
