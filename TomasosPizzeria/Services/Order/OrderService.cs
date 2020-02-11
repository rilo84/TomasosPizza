using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Data;
using TomasosPizzeria.Models;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Services
{
    public class OrderService : IOrderService
    {
        private readonly TomasosContext context;
        private readonly ISessionService sessionService;

        public OrderService(TomasosContext context, ISessionService sessionService)
        {
            this.context = context;
            this.sessionService = sessionService;
        }
        public void CreateOrder()
        {
            var cart = sessionService.GetCart();
            var user = sessionService.GetUser();

            AddOrder(cart,user);
            AddFood(cart, user);

            sessionService.ClearSessionData();
        }
        private void AddOrder(CartViewModel cart, ApplicationUser user)
        {
            var order = new Bestallning
            {
                BestallningDatum = DateTime.Now,
                Totalbelopp = (int)(cart.TotalAmount - cart.Discount),
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
