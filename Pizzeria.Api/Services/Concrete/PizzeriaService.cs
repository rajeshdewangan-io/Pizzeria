using Newtonsoft.Json;
using Pizzeria.Api.Models;
using Pizzeria.Api.Repository.Abstract;
using Pizzeria.Api.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pizzeria.Api.Services.Concrete
{
    public class PizzeriaService : IPizzeriaService
    {
        private readonly IPizzeriaRepository _pizzeriaRepository;
        public PizzeriaService(IPizzeriaRepository pizzeriaRepository)
        {
            _pizzeriaRepository = pizzeriaRepository;
        }
        public Models.Pizzeria GetAllPizzeria()
        {
            return _pizzeriaRepository.GetAllPizzeria();
        }

        public Pizzas GetPizzeria(int id)
        {
            return _pizzeriaRepository.GetPizzeria(id);
        }

        public int SavePizzaOrder(OrderPizza orderPizza)
        {
            CreateOrderAndSave(orderPizza);
            return orderPizza.OrderNumber;
        }

        private void CreateOrderAndSave(OrderPizza orderPizza)
        {
            orderPizza.OrderNumber = GenerateOrderNumber();
            var pizzeriaObj = _pizzeriaRepository.GetAllPizzeria();
            var pizza = pizzeriaObj.Pizzas.FirstOrDefault(p => p.Id == orderPizza.Id);
            decimal basePrice = pizza.Price;
            var selectedSize = pizza.Sizes.FirstOrDefault(m => m.Id == orderPizza.Size);
            var selectedSauce = pizza.Sauces.FirstOrDefault(m => m.Id == orderPizza.Sauce);
            decimal priceForExtraCheese = orderPizza.ExtraCheese ? 50 : 0;
            var selectedToppings = pizza.Toppings.Where(f => orderPizza.Toppings.Contains(f.Id));
            var totalPrice = GetTotalPrice(basePrice, selectedSize, selectedSauce, priceForExtraCheese, selectedToppings);
            var toppings = string.Join(",", selectedToppings.Select(s => s.Name));
            _pizzeriaRepository.SavePizzaOrder(orderPizza, toppings, selectedSauce.Name, selectedSize.Name, totalPrice);
        }

        private static decimal GetTotalPrice(decimal basePrice, Size selectedSize, Sauce selectedSauce, decimal priceForExtraCheese, IEnumerable<Topping> selectedToppings)
        {
            return basePrice + selectedSize.Price + priceForExtraCheese + selectedSauce.Price + selectedToppings.Sum(s => s.Price);
        }

        private int GenerateOrderNumber()
        {
            Random generator = new Random();
            string randomNumber = generator.Next(0, 1000000).ToString("D6");
            return Convert.ToInt32(randomNumber);
        }
    }
}
