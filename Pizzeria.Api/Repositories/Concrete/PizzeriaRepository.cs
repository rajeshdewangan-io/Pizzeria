using Newtonsoft.Json;
using Pizzeria.Api.Models;
using Pizzeria.Api.Repository.Abstract;
using System;
using System.IO;
using System.Linq;

namespace Pizzeria.Api.Repository.Concrete
{
    public class PizzeriaRepository : IPizzeriaRepository
    {
        private static readonly string pizzeriaData = "{\"pizzas\":[{\"id\":1001,\"type\":\"veg\",\"price\":290.0,\"name\":\"paneer tikka\",\"image\":\"https://image.shutterstock.com/image-photo/delicious-italian-pizza-over-white-600w-127528958.jpg\",\"description\":\"This is popular italian pizza flavoured with marinated tikka sauce and paneer\",\"sizes\":[{\"id\":1,\"name\":\"Small\",\"price\":50.0},{\"id\":2,\"name\":\"Medium\",\"price\":75.0},{\"id\":3,\"name\":\"Large\",\"price\":100.0}],\"ingredients\":[{\"id\":1,\"name\":\"dough/flour\"},{\"id\":4,\"name\":\"pizza saucce\"},{\"id\":5,\"name\":\"pizza sauce seasoning\"},{\"id\":6,\"name\":\"cheese\"}],\"sauces\":[{\"id\":1,\"name\":\"marinara\",\"price\":15.0},{\"id\":2,\"name\":\"cheese\",\"price\":20.0},{\"id\":3,\"name\":\"ranch\",\"price\":25.0},{\"id\":4,\"name\":\"others\",\"price\":30.0}],\"toppings\":[{\"id\":111,\"name\":\"Paneer\",\"price\":45.0},{\"id\":112,\"name\":\"Fried Onion\",\"price\":18.0},{\"id\":106,\"name\":\"Green olive\",\"price\":50.0},{\"id\":113,\"name\":\"Capsicum\",\"price\":15.0},{\"id\":110,\"name\":\"Red peprika\",\"price\":30.0}]},{\"id\":1002,\"type\":\"nonveg\",\"price\":350.0,\"name\":\"Chicken Italian \",\"image\":\"https://image.shutterstock.com/image-photo/pizza-600w-53553874.jpg\",\"description\":\"This is popular italian pizza flavoured with light sugary taste and creamy touch\",\"sizes\":[{\"id\":1,\"name\":\"Small\",\"price\":50.0},{\"id\":2,\"name\":\"Medium\",\"price\":75.0},{\"id\":3,\"name\":\"Large\",\"price\":100.0}],\"ingredients\":[{\"id\":2,\"name\":\"deep dish pizza mix\"},{\"id\":4,\"name\":\"pizza saucce\"},{\"id\":5,\"name\":\"pizza sauce seasoning\"},{\"id\":6,\"name\":\"cheese\"},{\"id\":8,\"name\":\"sugar and cinnomon blend\"},{\"id\":9,\"name\":\"plain butter\"}],\"sauces\":[{\"id\":1,\"name\":\"marinara\",\"price\":15.0},{\"id\":2,\"name\":\"cheese\",\"price\":20.0},{\"id\":3,\"name\":\"ranch\",\"price\":25.0},{\"id\":4,\"name\":\"others\",\"price\":30.0}],\"toppings\":[{\"id\":101,\"name\":\"Pepperoni\",\"price\":110.0},{\"id\":103,\"name\":\"Chicken Sausage\",\"price\":90.0},{\"id\":102,\"name\":\"Mushroom\",\"price\":35.0},{\"id\":113,\"name\":\"Capsicum\",\"price\":15.0}]},{\"id\":1003,\"type\":\"veg\",\"price\":310.0,\"name\":\"veggie supreme\",\"image\":\"https://image.shutterstock.com/image-photo/hot-pizza-pepperoni-sausage-on-600w-770556520.jpg\",\"description\":\"This is popular italian pizza flavoured with crushed garlic, with multiple herbs topped up with sweet corn\",\"sizes\":[{\"id\":1,\"name\":\"Small\",\"price\":50.0},{\"id\":2,\"name\":\"Medium\",\"price\":75.0},{\"id\":3,\"name\":\"Large\",\"price\":100.0}],\"ingredients\":[{\"id\":2,\"name\":\"deep dish pizza mix\"},{\"id\":4,\"name\":\"pizza saucce\"},{\"id\":5,\"name\":\"pizza sauce seasoning\"},{\"id\":6,\"name\":\"cheese\"},{\"id\":7,\"name\":\"garlic herbs\"},{\"id\":10,\"name\":\"flavored butter\"}],\"sauces\":[{\"id\":1,\"name\":\"marinara\",\"price\":15.0},{\"id\":2,\"name\":\"cheese\",\"price\":20.0},{\"id\":3,\"name\":\"ranch\",\"price\":25.0},{\"id\":4,\"name\":\"others\",\"price\":30.0}],\"toppings\":[{\"id\":112,\"name\":\"Fried Onion\",\"price\":18.0},{\"id\":114,\"name\":\"Sweet corn\",\"price\":38.0},{\"id\":102,\"name\":\"Mushroom\",\"price\":35.0},{\"id\":113,\"name\":\"Capsicum\",\"price\":15.0},{\"id\":105,\"name\":\"Black olive \",\"price\":0.0}]},{\"id\":1004,\"type\":\"nonveg\",\"price\":400.0,\"name\":\"Tripple chicken feast\",\"image\":\"https://image.shutterstock.com/image-photo/mixture-pizza-italian-food-600w-332497832.jpg\",\"description\":\"This is popular italian pizza flavoured with unique greek dressing topped up with keema and meat ball\",\"sizes\":[{\"id\":1,\"name\":\"Small\",\"price\":50.0},{\"id\":2,\"name\":\"Medium\",\"price\":75.0},{\"id\":3,\"name\":\"Large\",\"price\":100.0}],\"ingredients\":[{\"id\":3,\"name\":\"low carb pizza dough\"},{\"id\":4,\"name\":\"pizza saucce\"},{\"id\":5,\"name\":\"pizza sauce seasoning\"},{\"id\":6,\"name\":\"cheese\"},{\"id\":13,\"name\":\"greek dressing\"},{\"id\":11,\"name\":\"cajun\"}],\"sauces\":[{\"id\":1,\"name\":\"marinara\",\"price\":15.0},{\"id\":2,\"name\":\"cheese\",\"price\":20.0},{\"id\":3,\"name\":\"ranch\",\"price\":25.0},{\"id\":4,\"name\":\"others\",\"price\":30.0}],\"toppings\":[{\"id\":115,\"name\":\"Chicken keema\",\"price\":99.0},{\"id\":112,\"name\":\"Fried Onion\",\"price\":18.0},{\"id\":116,\"name\":\"Chicken Meat ball\",\"price\":105.0},{\"id\":113,\"name\":\"Capsicum\",\"price\":15.0},{\"id\":114,\"name\":\"Sweet corn\",\"price\":38.0}]},{\"id\":1005,\"type\":\"nonveg\",\"price\":625.0,\"name\":\"Ultimate chicken\",\"image\":\"https://image.shutterstock.com/image-photo/pizza-margherita-italian-600w-246331354.jpg\",\"description\":\"This is popular italian pizza flavoured with BBA sauce, flavored butter. it has spongy base which gives unique taste with multiple toppingss\",\"sizes\":[{\"id\":1,\"name\":\"Small\",\"price\":50.0},{\"id\":2,\"name\":\"Medium\",\"price\":75.0},{\"id\":3,\"name\":\"Large\",\"price\":100.0}],\"ingredients\":[{\"id\":2,\"name\":\"deep dish pizza mix\"},{\"id\":4,\"name\":\"pizza saucce\"},{\"id\":5,\"name\":\"pizza sauce seasoning\"},{\"id\":6,\"name\":\"cheese\"},{\"id\":12,\"name\":\"BBQ sauce\"},{\"id\":11,\"name\":\"cajun\"},{\"id\":10,\"name\":\"flavored butter\"}],\"sauces\":[{\"id\":1,\"name\":\"marinara\",\"price\":15.0},{\"id\":2,\"name\":\"cheese\",\"price\":20.0},{\"id\":3,\"name\":\"ranch\",\"price\":25.0},{\"id\":4,\"name\":\"others\",\"price\":30.0}],\"toppings\":[{\"id\":101,\"name\":\"Pepperoni\",\"price\":110.0},{\"id\":112,\"name\":\"Fried Onion\",\"price\":18.0},{\"id\":116,\"name\":\"Chicken Meat ball\",\"price\":105.0},{\"id\":103,\"name\":\"Chicken Sausage\",\"price\":90.0},{\"id\":115,\"name\":\"Chicken keema\",\"price\":99.0}]}]}";
        public Models.Pizzeria GetAllPizzeria()
        {
            var pizzeriaObj = JsonConvert.DeserializeObject<Models.Pizzeria>(pizzeriaData);
            return pizzeriaObj;
        }
       
        public Pizzas GetPizzeria(int id)
        {
            var pizzeriaObj = JsonConvert.DeserializeObject<Models.Pizzeria>(pizzeriaData);
            return pizzeriaObj.Pizzas.FirstOrDefault(p => p.Id == id);
        }

        public bool SavePizzaOrder(OrderPizza orderPizza, string toppings, string sauceName, string sizeName, decimal totalPrice)
        {
            using (StreamWriter writer = File.AppendText("pizza-order.txt"))
            {
                writer.WriteLine("----------Pizza Order Number : {0}----------------------", orderPizza.OrderNumber);
                writer.WriteLine("Name:{0}, Type:{1}, Size:{2}, Sauce:{3}, Extra Cheese:{4}", orderPizza.Name, orderPizza.Type, sizeName, sauceName, orderPizza.ExtraCheese ? "Yes" : "No");
                writer.WriteLine("Toppings:{0}", toppings);
                writer.WriteLine("Name:{0}, Contact Number:{1}, Order Date:{2}", orderPizza.CustomerName, orderPizza.ContactNumber, DateTime.Now);
                writer.WriteLine("Total Amount:{0}", totalPrice);
            }
            return true;
        }
    }
}
