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
        public List<Bestallning> GetAllOrders()
        {
            return context.Bestallning.ToList();
        }
    }
}
