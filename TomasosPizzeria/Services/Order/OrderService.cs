using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Data;
using TomasosPizzeria.Models;
using TomasosPizzeria.Repositories;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Services
{
    public class OrderService : IOrderService
    {
        private readonly TomasosContext context;
        private readonly ISessionService sessionService;
        private readonly IUserRepository userRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public OrderService(TomasosContext context, ISessionService sessionService, IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.sessionService = sessionService;
            this.userRepository = userRepository;
            this.userManager = userManager;
        }
        public void CreateOrder()
        {
            var cart = sessionService.GetCart();
            var user = sessionService.GetUser();

            AddOrder(cart,user);
            AddFood(cart, user);

            cart = new CartViewModel();
            sessionService.SetCart(cart);
            
        }
        private void AddOrder(CartViewModel cart, ApplicationUser user)
        {
            var order = new Bestallning
            {
                BestallningDatum = DateTime.Now,
                Totalbelopp = (int)(cart.TotalAmount - cart.Discount -cart.BonusMoney),
                Levererad = false,
                KundId = user.Id
            };

            context.Bestallning.Add(order);
            context.SaveChanges();
        }
        private void AddFood(CartViewModel cart, ApplicationUser user)
        {
            var foodId = context.Bestallning.Where(k => k.KundId == user.Id).OrderByDescending(o => o.BestallningId).FirstOrDefault();

            foreach (var food in cart.Food)
            {
                var foodItem = context.Matratt.FirstOrDefault(f => f.MatrattId == food.FoodId);
                context.BestallningMatratt.Add(new BestallningMatratt
                {
                    Antal = food.OrderAmount,
                    BestallningId = foodId.BestallningId,
                    MatrattId = foodItem.MatrattId
                });
                context.SaveChanges();
            }
        }

    }
}
