using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Repositories
{
    public interface IFoodRepository
    {
        Menu GetMenu();
    }
}
