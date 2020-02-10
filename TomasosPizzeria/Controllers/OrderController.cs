using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TomasosPizzeria.Repositories;
using TomasosPizzeria.Services;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Controllers
{
    public class OrderController : Controller
    {
        private IFoodRepository _foodRepository;
        private readonly ISessionService sessionService;
        private readonly ISelectService selectService;
        private readonly ICartService cartService;

        public OrderController(
            IFoodRepository foodRepository, 
            ISessionService sessionService,
            ISelectService selectService, 
            ICartService cartService)
        {
            _foodRepository = foodRepository;
            this.sessionService = sessionService;
            this.selectService = selectService;
            this.cartService = cartService;
        }

        [HttpGet]
        public IActionResult Order()
        {
            var model = new OrderViewModel();

            model.Menu = _foodRepository.GetMenu();
            model.OrderAmounts = selectService.GetListNumbers(1, 10);
            model.Cart = sessionService.TryGetCart(model.Cart);

            return View(model);
        }

        [HttpPost]
        public IActionResult AddToCart(int Id, OrderViewModel model)
        {
            model.Cart = sessionService.TryGetCart(model.Cart);
            var foodItem = cartService.MakeFoodItem(Id, model);
            cartService.AddFood(foodItem, model);
            sessionService.SetCart(model.Cart);

            return ViewComponent("OrderCart", model.Cart);
        }
    }
}