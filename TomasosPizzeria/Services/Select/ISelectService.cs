using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Models;

namespace TomasosPizzeria.Services
{
    public interface ISelectService
    {
        List<SelectListItem> GetListNumbers(int start, int stop);
        List<SelectListItem> GetListFoods();
        List<SelectListItem> GetListCategory(string defaultValue = null);
        List<SelectListItem> GetListIngredients();

        List<SelectListItem> GetListIngredients(int foodId);
    }
}
