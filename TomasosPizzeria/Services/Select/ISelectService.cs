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
        List<SelectListItem> GetListCategory();
        List<SelectListItem> GetListIngredients();
    }
}
