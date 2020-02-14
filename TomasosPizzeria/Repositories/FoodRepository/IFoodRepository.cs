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
        Matratt GetFoodById(int id);
        MatrattTyp GetFoodCategory(int foodId);
        List<Matratt> GetAllFoods();
        List<MatrattTyp> GetAllCategories();
        List<Produkt> GetAllProducts();
        List<Produkt> GetAllProducts(int foodId);
        void UpdateFood(Matratt food);
        void AddIngredient(int foodId, int ingredientId);
        void RemoveIngredient(int foodId, int ingredientId);
    }
}
