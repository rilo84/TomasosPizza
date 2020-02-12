using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Models;

namespace TomasosPizzeria.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TomasosContext context;

        public OrderRepository(TomasosContext context)
        {
            this.context = context;
        }

        public List<Bestallning> GetAllCustomerOrders(string customerId)
        {
            return context.Bestallning.Where(o => o.KundId == customerId).OrderByDescending(o => o.BestallningDatum).ToList();
   
        }

        public List<Bestallning> GetAllOrders()
        {
            return context.Bestallning.OrderByDescending(o => o.BestallningDatum).ToList();
        }

        public List<BestallningMatratt> GetOrderFood(int orderID)
        {
            return context.BestallningMatratt.Where(o => o.BestallningId == orderID).ToList();
        }

        public List<Matratt> GetOrderFoodDetails(int orderID)
        {
            return context.BestallningMatratt.Where(f => f.BestallningId == orderID).Select(f => f.Matratt).ToList();
        }
    }
}
