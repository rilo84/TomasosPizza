using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.ViewModels
{
    public class OrderCart
    {
        public DateTime Date { get; set; }
        public List<Food> FoodItems { get; set; }

    }
}
