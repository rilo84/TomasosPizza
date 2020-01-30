using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Models;

namespace TomasosPizzeria.Repositories
{
    public interface ICustomerRepository
    {
        void AddCustomer(Kund customer);

        Kund LoginCustomer(Kund customer);
    }
}
