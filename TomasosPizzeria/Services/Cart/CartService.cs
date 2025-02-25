﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Data;
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

        public void AddBonus(CartViewModel model)
        {
            int bonus = 0;
            model.Food.ForEach(f => bonus += f.OrderAmount * 10);

            model.TotalBonus = bonus;
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

        public void CheckBonus(CartViewModel model)
        {
            if (model.CurrentBonus >= 100)
            {
                var food = model.Food.OrderBy(f => f.Price).FirstOrDefault();

                model.BonusMoney = food.Price;
            }
        }

        public void CheckDiscount(CartViewModel model)
        {
            int amountFood = 0;
            model.Food.ForEach(f => amountFood += f.OrderAmount);

            if (amountFood > 2)
            {
                model.Discount = model.TotalAmount * 0.2m;
            }
            else
            {
                model.Discount = 0;
            }
        }

        public Food MakeFoodItem(int id, OrderViewModel model)
        {
            var foodContext = foodRepository.GetFoodById(id);
            
            var foodItem = new Food();
            foodItem.FoodId = id;
            foodItem.Name = foodContext.MatrattNamn;
            foodItem.OrderAmount = model.OrderAmount;
            foodItem.Price = foodContext.Pris;
            foodItem.FoodTotal += foodItem.Price * foodItem.OrderAmount;

            return foodItem;
        }

        public void RemoveFood(int id, OrderViewModel model)
        {
            var food = model.Cart.Food.FirstOrDefault(f => f.FoodId == id);
            var foodIndex = model.Cart.Food.IndexOf(food);

            if (food.OrderAmount > 1)
            {
                food.OrderAmount -= 1;
                food.FoodTotal -= food.Price;
                model.Cart.TotalAmount -= food.Price;
                model.Cart.Food[foodIndex] = food;
            }
            else
            {
                model.Cart.TotalAmount -= food.Price;
                model.Cart.Food.Remove(food);
            }
                
        }

        public void UseBonus(ApplicationUser user, CartViewModel cart)
        {
            if (user.BonusPoints >= 100)
            {
                user.BonusPoints += cart.TotalBonus - 100;
            }
            else
            {
                user.BonusPoints += cart.TotalBonus;
            }
        }
    }
}
