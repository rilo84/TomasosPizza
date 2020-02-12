using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.ViewModels;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Models;

namespace TomasosPizzeria.ViewComponents.OrderInfo
{
    public class OrderInfo:ViewComponent
    {
        public IViewComponentResult Invoke(OrderDetailsViewModel model)
        {
            return View("OrderInfo", model);
        }
    }
}
