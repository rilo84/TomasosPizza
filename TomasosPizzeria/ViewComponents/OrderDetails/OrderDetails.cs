using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace TomasosPizzeria.ViewComponents.OrderDetails
{
    public class OrderDetails:ViewComponent
    {
        public IViewComponentResult Invoke(AdminOrderViewModel model)
        {
            return View("OrderDetails", model);
        }
    }
}
