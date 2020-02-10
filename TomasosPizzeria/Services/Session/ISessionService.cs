using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Data;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Services
{
    public interface ISessionService
    {
        ApplicationUser GetUser();
        void SetUser(ApplicationUser user);
        CartViewModel TryGetCart(CartViewModel cart);
        void SetCart(CartViewModel cart);
        void ClearSessionData();
    }
}
