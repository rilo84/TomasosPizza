using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.ViewModels
{
    public class FoodCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Food> FoodItems { get; set; }

        public FoodCategory()
        {
            FoodItems = new List<Food>();
        }

    }
}
