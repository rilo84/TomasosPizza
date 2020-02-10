using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Repositories;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Services
{
    public class CartService : ICartService
    {
        private readonly IFoodRepository foodRepository;

        public CartService(IFoodRepository foodRepository)
        {
            this.foodRepository = foodRepository;
        }

        public void AddFood(Food foodItem, OrderViewModel model)
        {
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
        }

        public Food MakeFoodItem(int id, OrderViewModel model)
        {
            var foodContext = foodRepository.GetFoodById(id);
            
            var foodItem = new Food();
            foodItem.Name = foodContext.MatrattNamn;
            foodItem.OrderAmount = model.OrderAmount;
            foodItem.Price = foodContext.Pris;
            foodItem.FoodTotal += foodItem.Price * foodItem.OrderAmount;

            return foodItem;
        }
    }
}
