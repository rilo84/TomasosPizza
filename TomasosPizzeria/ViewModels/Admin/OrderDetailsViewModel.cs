using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Models;

namespace TomasosPizzeria.ViewModels
{
    public class OrderDetailsViewModel
    {
        public int OrderId { get; set; }
        public List<Matratt> Foods { get; set; }

        public OrderDetailsViewModel()
        {
            Foods = new List<Matratt>();
        }
    }
}
