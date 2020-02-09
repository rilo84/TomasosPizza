using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.ViewModels
{
    public class OrderViewModel
    {
        public Menu Menu { get; set; }
        public int OrderAmount { get; set; }

        public string FoodId { get; set; }
        public List<SelectListItem> OrderAmounts { get; set; }

    }
}
