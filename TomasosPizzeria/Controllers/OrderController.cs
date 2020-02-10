using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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

        [HttpGet]
        public IActionResult Order()
        {
            var model = new OrderViewModel();
            model.Menu = _foodRepository.GetMenu();
            model.OrderAmounts = new List<SelectListItem>();

            for (int i = 0; i < 10; i++)
            {
                model.OrderAmounts.Add(new SelectListItem { Text = $"{i + 1}", Value = $"{i + 1}" });
            }

            var cartJson = HttpContext.Session.GetString("cartData");

            if (cartJson != null)
            {
                model.Cart = JsonConvert.DeserializeObject<CartViewModel>(cartJson);
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AddToCart(int Id, OrderViewModel model)
        {
            var food = _foodRepository.GetFoodById(Id);
            var foodItem = new Food();
            foodItem.Name = food.MatrattNamn;
            foodItem.OrderAmount = model.OrderAmount;
            foodItem.Price = food.Pris;

            foodItem.FoodTotal += foodItem.Price * foodItem.OrderAmount;

            var cartJson = HttpContext.Session.GetString("cartData");

            if (cartJson != null)
            {
                model.Cart = JsonConvert.DeserializeObject<CartViewModel>(cartJson);
            }
           
            var existingFood = model.Cart.Food.FirstOrDefault(f => f.Name == foodItem.Name);

            if (existingFood != null)
            {
                existingFood.OrderAmount += foodItem.OrderAmount;
                existingFood.FoodTotal += foodItem.FoodTotal;
            }
            else
            {
                model.Cart.Food.Add(foodItem);
            }

            model.Cart.TotalAmount += foodItem.Price * foodItem.OrderAmount;

            cartJson = JsonConvert.SerializeObject(model.Cart);
            HttpContext.Session.SetString("cartData",cartJson);

            return ViewComponent("OrderCart", model.Cart);
        }
    }
}