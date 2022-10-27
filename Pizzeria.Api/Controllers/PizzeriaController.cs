using Microsoft.AspNetCore.Mvc;
using Pizzeria.Api.Models;
using Pizzeria.Api.Services.Abstract;

namespace Pizzeria.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzeriaController : ControllerBase
    {
        private readonly IPizzeriaService _pizzeriaService;

        public PizzeriaController(IPizzeriaService pizzeriaService)
        {
            _pizzeriaService = pizzeriaService;
        }

        // GET api/pizzeria
        [HttpGet]
        public ActionResult<Models.Pizzeria> GetAllPizzeria()
        {
            var pizzeriaObj = _pizzeriaService.GetAllPizzeria();
            return pizzeriaObj;
        }

        // GET api/pizzeria
        [HttpGet("{id}")]
        public ActionResult<Pizzas> GetPizzeria(int id)
        {
            var pizza = _pizzeriaService.GetPizzeria(id);
            return pizza;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<int> SavePizzaOrder([FromBody] OrderPizza orderPizza)
        {
            var orderNumber = _pizzeriaService.SavePizzaOrder(orderPizza);
            return orderNumber;
        }


    }
}