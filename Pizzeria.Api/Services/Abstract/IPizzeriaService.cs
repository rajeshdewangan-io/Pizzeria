using Pizzeria.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Api.Services.Abstract
{
   public interface IPizzeriaService
    {
        Models.Pizzeria GetAllPizzeria();
        Pizzas GetPizzeria(int id);
        int SavePizzaOrder(OrderPizza orderPizza);
    }
}
