using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.ViewModels
{
    public class OrderViewModel
    {
        public Menu Menu { get; set; }
        public OrderCart OrderCart { get; set; }

    }
}
