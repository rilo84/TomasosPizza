using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Models;

namespace TomasosPizzeria.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private TomasosContext _context;
        public CustomerRepository(TomasosContext context)
        {
            _context = context;
        }
        public void AddCustomer(Kund customer)
        {
            _context.Kund.Add(customer);
            _context.SaveChanges();
        }

        public Kund LoginCustomer(Kund customer)
        {
            return _context.Kund
                .FirstOrDefault(c => c.AnvandarNamn == customer.AnvandarNamn && c.Losenord == customer.Losenord);
        }
    }
}
