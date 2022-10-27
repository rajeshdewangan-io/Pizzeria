using Microsoft.AspNetCore.Mvc;
using Moq;
using Pizzeria.Api.Controllers;
using Pizzeria.Api.Models;
using Pizzeria.Api.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Pizzeria.Api.Test.Controller
{
    public class PizzeriaControllerTests
    {
        private readonly Mock<IPizzeriaService> _mockService;
        private readonly PizzeriaController _controller;
        public PizzeriaControllerTests()
        {
            _mockService = new Mock<IPizzeriaService>();
            _controller = new PizzeriaController(_mockService.Object);
        }

        [Fact]
        public void GetAllPizzeria_Test()
        {
            _mockService.Setup(ser => ser.GetAllPizzeria()).Returns(new Models.Pizzeria() { Pizzas = new List<Models.Pizzas>() { new Models.Pizzas(), new Models.Pizzas() } });
            var result = _controller.GetAllPizzeria();
            var pizzeria = Assert.IsType<Models.Pizzeria>(result.Value);
            var pizzas = Assert.IsType<List<Pizzas>>(pizzeria.Pizzas);
            Assert.Equal(2, pizzas.Count);
        }

        [Fact]
        public void GetPizzeria_Test()
        {
            var testPizza = new Models.Pizzas() { Description = "test desc", Id = 1001, Name = "pizza mania", Type = "veg" };
            _mockService.Setup(ser => ser.GetPizzeria(It.IsAny<int>())).Returns(testPizza);
            var result = _controller.GetPizzeria(125);
            var pizzas = Assert.IsType<Models.Pizzas>(result.Value);
            Assert.Equal(testPizza.Name, pizzas.Name);
            Assert.Equal(testPizza.Id, pizzas.Id);
            Assert.Equal(testPizza.Description, pizzas.Description);
            Assert.Equal(testPizza.Type, pizzas.Type);
        }

        [Fact]
        public void SavePizzaOrder_Test()
        {
            var testPizzaOrderNumber = 123213;
            _mockService.Setup(ser => ser.SavePizzaOrder(It.IsAny<OrderPizza>())).Returns(testPizzaOrderNumber);
            var result = _controller.SavePizzaOrder(new OrderPizza());
            var pizzaOrderNumber = Assert.IsType<int>(result.Value);
            Assert.Equal(testPizzaOrderNumber, pizzaOrderNumber);
           
        }
    }
}
