using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.Services
{
    public class SelectService : ISelectService
    {
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
