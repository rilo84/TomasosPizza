using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Services
{
    public interface ICartService
    {
        Food MakeFoodItem(int id, OrderViewModel model);

        void AddFood(Food foodItem , OrderViewModel model);
    }
}
