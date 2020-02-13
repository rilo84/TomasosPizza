using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Models;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Repositories
{
    public interface IFoodRepository
    {
        Menu GetMenu();
        Matratt GetFoodById(int Id);
        List<Matratt> GetAllFoods();
        List<MatrattTyp> GetAllCategories();
        List<Produkt> GetAllProducts();
    }
}
