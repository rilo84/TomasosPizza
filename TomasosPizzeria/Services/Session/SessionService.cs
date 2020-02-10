using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Data;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Services
{
    public class SessionService:ISessionService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession _session => httpContextAccessor.HttpContext.Session;
        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public ApplicationUser GetUser()
        {
            var customerJson = _session.GetString("customerData");
            return JsonConvert.DeserializeObject<ApplicationUser>(customerJson);
        }

        public void ClearSessionData()
        {
            _session.Clear();
        }

        public void SetUser(ApplicationUser user)
        {
            var customerJson = JsonConvert.SerializeObject(user);
            _session.SetString("customerData", customerJson);
        }

        public CartViewModel TryGetCart(CartViewModel cart)
        {
            var cartJson = _session.GetString("cartData");

            if (cartJson != null)
            {
                return JsonConvert.DeserializeObject<CartViewModel>(cartJson);
            }

            return cart;
        }

        public void SetCart(CartViewModel cart)
        {
            var cartJson = JsonConvert.SerializeObject(cart);
            _session.SetString("cartData", cartJson);
        }
    }
}
