using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Data;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Services
{
    public interface ICartService
    {
        Food MakeFoodItem(int id, OrderViewModel model);

        void RemoveFood(int id, OrderViewModel model);

        void AddFood(Food foodItem , OrderViewModel model);

        void CheckDiscount(CartViewModel model);
        void CheckBonus(CartViewModel model);

        void AddBonus(CartViewModel model);
    }
}
