using Pizzeria.Api.Models;

namespace Pizzeria.Api.Repository.Abstract
{
    public interface IPizzeriaRepository
    {
        Models.Pizzeria GetAllPizzeria();
        Pizzas GetPizzeria(int id);
        bool SavePizzaOrder(OrderPizza orderPizza, string toppings, string sauceName, string sizeName, decimal totalPrice);
    }
}
