using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Models;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Repositories
{
    public class FoodRepository:IFoodRepository
    {
        private TomasosContext _context;
        public FoodRepository(TomasosContext context)
        {
            _context = context;
        }

        public List<MatrattTyp> GetAllCategories()
        {
            return _context.MatrattTyp.ToList();
        }

        public List<Matratt> GetAllFoods()
        {
            return _context.Matratt.ToList();
        }

        public List<Produkt> GetAllProducts()
        {
            return _context.Produkt.ToList();
        }

        public Matratt GetFoodById(int Id)
        {
            return _context.Matratt.FirstOrDefault(food => food.MatrattId == Id);
        }

        public Menu GetMenu()
        {
            var menu = new Menu();

            AddCategories(menu);

            AddFood(menu.FoodCategory);

            foreach (var category in menu.FoodCategory)
            {
                AddIngredient(category.FoodItems);
            }

            return menu;
        }

        private void AddCategories(Menu menu)
        {
            foreach (var foodCategory in _context.MatrattTyp)
            {
                var category = new FoodCategory();
                category.CategoryId = foodCategory.MatrattTyp1;
                category.CategoryName = foodCategory.Beskrivning;
                menu.FoodCategory.Add(category);
            }
        }

        private void AddFood(List<FoodCategory> foodCategory)
        {
            foreach (var category in foodCategory)
            {
                foreach (var food in _context.Matratt)
                {
                    if (category.CategoryId == food.MatrattTyp)
                    {
                        var foodItem = new Food();

                        foodItem.Price = food.Pris;

                        foodItem.FoodId = food.MatrattId;

                        foodItem.Name = food.MatrattNamn;

                        foodItem.Description = food.Beskrivning;

                        category.FoodItems.Add(foodItem);
                    }
                }

            }
        }

        private void AddIngredient(List<Food> foodItems)
        {
            foreach (var food in foodItems)
            {
                var ingredients = _context.MatrattProdukt.Where(p => p.MatrattId == food.FoodId).Select(p => p.Produkt).ToList();

                foreach (var ingredient in ingredients)
                {
                    food.Ingredients += $"{ingredient.ProduktNamn}, ";
                }
            }
        }

    }
}
