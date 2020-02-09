using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.ViewComponents.OrderCart
{
    public class OrderCart:ViewComponent
    {
        public IViewComponentResult Invoke(CartViewModel cart)
        {
            return View("OrderCart", cart);
        }
    }
}
