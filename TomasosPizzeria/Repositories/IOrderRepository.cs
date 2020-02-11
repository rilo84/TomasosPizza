﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Models;

namespace TomasosPizzeria.Repositories
{
    public interface IOrderRepository
    {
        List<Bestallning> GetAllOrders();
    }
}
