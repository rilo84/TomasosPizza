using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TomasosPizzeria.Data;
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
        private readonly IOrderService orderService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrderController(
            IFoodRepository foodRepository, 
            ISessionService sessionService,
            ISelectService selectService, 
            ICartService cartService,
            IUserRepository userRepository,
            IOrderService orderService,
            UserManager<ApplicationUser> userManager)
        {
            _foodRepository = foodRepository;
            this.sessionService = sessionService;
            this.selectService = selectService;
            this.cartService = cartService;
            this.userRepository = userRepository;
            this.orderService = orderService;
            this.userManager = userManager;
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
            

            var customer = sessionService.GetUser();
            if (await userRepository.IsPremium(customer))
            {
                model.Cart.IsPremium = true;
                cartService.GetCurrentBonus(model.Cart);
                cartService.CheckDiscount(model.Cart);
                cartService.CheckBonus(model.Cart);
                cartService.AddBonus(model.Cart);
                
            }

            sessionService.SetCart(model.Cart);

            return ViewComponent("OrderCart", model.Cart);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int Id, OrderViewModel model)
        {
            model.Cart = sessionService.TryGetCart(model.Cart);
            cartService.RemoveFood(Id, model);
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
        public async Task<IActionResult> OrderConfirmation()
        {
            var user = await userManager.GetUserAsync(User);
            var cart = sessionService.GetCart();
            user.BonusPoints += cart.TotalBonus;

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                orderService.CreateOrder();
            }
            
            return View();
        }
    }
}