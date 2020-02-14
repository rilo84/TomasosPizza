using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Models;
using TomasosPizzeria.Repositories;

namespace TomasosPizzeria.Services
{
    public class SelectService : ISelectService
    {
        private readonly IFoodRepository foodRepository;

        public SelectService(IFoodRepository foodRepository)
        {
            this.foodRepository = foodRepository;
        }
        public List<SelectListItem> GetListCategory(string defaultValue)
        {
            var listNums = new List<SelectListItem>();
            var categories = foodRepository.GetAllCategories();

            foreach (var category in categories)
            {
                if (category.Beskrivning == defaultValue)
                {
                    listNums.Add(new SelectListItem(category.Beskrivning, category.MatrattTyp1.ToString(),true));
                }
                else
                {
                    listNums.Add(new SelectListItem(category.Beskrivning, category.MatrattTyp1.ToString(),false));
                }
            }
       
            return listNums;

        }

        public List<SelectListItem> GetListFoods()
        {
            var listNums = new List<SelectListItem>();
            var foods = foodRepository.GetAllFoods();

            foreach (var food in foods)
            {
                listNums.Add(new SelectListItem { Text = food.MatrattNamn, Value = food.MatrattId.ToString() });
            }
            return listNums;
        }

        public List<SelectListItem> GetListIngredients()
        {
            var listNums = new List<SelectListItem>();
            var ingredients = foodRepository.GetAllProducts();

            foreach (var ingredient in ingredients)
            {
                listNums.Add(new SelectListItem { Text = ingredient.ProduktNamn, Value = ingredient.ProduktId.ToString() });
            }
            return listNums;
        }

        public List<SelectListItem> GetListIngredients(int foodId)
        {
            var listNums = new List<SelectListItem>();
            var ingredients = foodRepository.GetAllProducts(foodId);

            foreach (var ingredient in ingredients)
            {
                listNums.Add(new SelectListItem { Text = ingredient.ProduktNamn, Value = ingredient.ProduktId.ToString() });
            }
            return listNums;
        }

        public List<SelectListItem> GetListNumbers(int start, int stop)
        {
            var listNums = new List<SelectListItem>();

            for (int i = (start - 1); i < stop; i++)
            {
                listNums.Add(new SelectListItem { Text = $"{i + 1}", Value = $"{i + 1}" });
            }

            return listNums;
        }
    }
}
