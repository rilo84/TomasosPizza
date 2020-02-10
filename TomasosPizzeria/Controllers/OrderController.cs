using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TomasosPizzeria.Models;
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
        private readonly IUserRepository userRepository;
        private readonly TomasosContext context;

        public OrderController(
            IFoodRepository foodRepository, 
            ISessionService sessionService,
            ISelectService selectService, 
            ICartService cartService,
            IUserRepository userRepository,
            TomasosContext context)
        {
            _foodRepository = foodRepository;
            this.sessionService = sessionService;
            this.selectService = selectService;
            this.cartService = cartService;
            this.userRepository = userRepository;
            this.context = context;
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
        public async Task<IActionResult> AddToCart(int Id, OrderViewModel model)
        {
            model.Cart = sessionService.TryGetCart(model.Cart);
            var foodItem = cartService.MakeFoodItem(Id, model);
            cartService.AddFood(foodItem, model);
            sessionService.SetCart(model.Cart);

            var customer = sessionService.GetUser();
            if (await userRepository.IsPremium(customer))
            {
                model.Cart.IsPremium = true;
                cartService.CheckDiscount(model.Cart);
                cartService.CheckBonus(model.Cart);
                cartService.AddBonus(model.Cart);
            }
            
            return ViewComponent("OrderCart", model.Cart);
        }

        [HttpPost]
        public IActionResult OrderConfirmation()
        {
            var user = sessionService.GetUser();
            var cart = sessionService.GetCart();

            var order = new Bestallning
            {
                BestallningDatum = DateTime.Now,
                Totalbelopp = 1000,
                Levererad = false,
                KundId = user.Id
            };


            context.Bestallning.Add(order);
            context.SaveChanges();

            return View();
        }
    }
}