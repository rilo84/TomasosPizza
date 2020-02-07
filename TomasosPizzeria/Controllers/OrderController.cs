using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Repositories;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Controllers
{
    public class OrderController : Controller
    {
        private IFoodRepository _foodRepository;
        public OrderController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }
        public IActionResult Order()
        {
            var model = new OrderViewModel();
            model.Menu = _foodRepository.GetMenu();
            
            return View(model);
        }
    }
}