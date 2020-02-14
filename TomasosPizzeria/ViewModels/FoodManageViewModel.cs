using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public List<SelectListItem> FoodIngredientSelectList { get; set; }
        public int FoodId { get; set; }
        public MatrattTyp FoodCategory { get; set; }
        
        public int IngredientId { get; set; }
        public Matratt Food { get; set; }

        [Required(ErrorMessage = "Pris är obligatoriskt")]
        public int FoodPrice { get; set; }

        [Required(ErrorMessage = "Maträttens namn är obligatoriskt")]
        [StringLength(50, ErrorMessage = "Maträttens namn får vara max 50 tecken")]
        public string FoodName { get; set; }

        [Required(ErrorMessage = "Ingrediensnamn är obligatoriskt")]
        [StringLength(50, ErrorMessage = "Ingrediensnamn får vara max 50 tecken")]
        public string IngredientName { get; set; }

    }
}
