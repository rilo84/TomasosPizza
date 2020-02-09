using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.ViewModels
{
    public class Menu
    {
        public List<FoodCategory> FoodCategory { get; set; }
        public Menu()
        {
            FoodCategory = new List<FoodCategory>();
        }
    }
}
