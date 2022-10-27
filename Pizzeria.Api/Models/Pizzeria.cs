using System.Collections.Generic;

namespace Pizzeria.Api.Models
{
    public class Size
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Sauce
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Topping
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Pizzeria
    {
        public List<Pizzas> Pizzas { get; set; }
    }
    public class Pizzas
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public List<Size> Sizes { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Sauce> Sauces { get; set; }
        public List<Topping> Toppings { get; set; }
    }

    public class OrderPizza
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public int Sauce { get; set; }
        public bool ExtraCheese { get; set; }
        public int[] Toppings { get; set; }
        public int ContactNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalPrice { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int OrderNumber { get; set; }
    }
}
