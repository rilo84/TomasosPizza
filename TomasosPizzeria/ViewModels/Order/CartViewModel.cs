using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.ViewModels
{
    public class CartViewModel
    {
        public List<Food> Food { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalBonus { get; set; }

        public CartViewModel()
        {
            Food = new List<Food>();
        }
    }
}
