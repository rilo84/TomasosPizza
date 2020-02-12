using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Models;

namespace TomasosPizzeria.Repositories
{
    public interface IOrderRepository
    {
        List<Bestallning> GetAllOrders();

        List<Bestallning> GetAllCustomerOrders(string customerId);
        List<BestallningMatratt> GetOrderFood(int orderId);
        List<Matratt> GetOrderFoodDetails(int orderId);

        void SetDelivered(int orderId);
    }
}
