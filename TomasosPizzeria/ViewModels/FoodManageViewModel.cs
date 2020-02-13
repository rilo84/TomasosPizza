using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Models;

namespace TomasosPizzeria.ViewModels
{
    public class FoodManageViewModel
    {
        public List<Matratt> Foods { get; set; }
        public List<SelectListItem> CategorySelectList { get; set; }

        public List<SelectListItem> FoodSelectList { get; set; }

        public List<SelectListItem> IngredientSelectList { get; set; }
    }
}
