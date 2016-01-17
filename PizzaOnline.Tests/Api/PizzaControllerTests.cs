using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using FakeItEasy;
using NUnit.Framework;
using PizzaOnline.Api.Controllers;
using PizzaOnline.Model;
using PizzaOnline.Services;

namespace PizzaOnline.Tests.Api
{
    [TestFixture]
    public class PizzaControllerTests : BaseControllerTests
    {
        private IPizzaService _pizzaService;
        private PizzaController _sut;

        [SetUp]
        public void SetUp()
        {
            _pizzaService = A.Fake<IPizzaService>();
            _sut = new PizzaController(_pizzaService)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [Test]
        public async void GetPizza_ShouldReturnStatusCodeNotFound_WhenThePizzaWasNotFound()
        {
            A.CallTo(() => _pizzaService.Get(A<int>._)).Returns(null);

            var result = _sut.GetPizza(int.MaxValue);

            var response = await result.ExecuteAsync(CancellationToken.None);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async void GetPizza_ShouldReturnStatusCodeOk_WhenThePizzaWasFound()
        {
            var pizza = new Pizza
            {
                Id = 2,
                Name = "pizzaName",
                Price = 123.2M
            };
            A.CallTo(() => _pizzaService.Get(A<int>._)).Returns(pizza);

            var result = _sut.GetPizza(int.MaxValue);

            var response = await result.ExecuteAsync(CancellationToken.None);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async void GetAllPizzas_ShouldReturnStatusCodeOk_WhenThePizzatWasFound()
        {
            var pizzas = new List<Pizza>
            {
                new Pizza
                {
                    Id = 2,
                    Name = "pizzaName",
                    Price = 123.2M
                },
                new Pizza
                {
                    Id = 3,
                    Name = "pizzaName4",
                    Price = 623.2M
                }
            };
            A.CallTo(() => _pizzaService.GetAllPizzas()).Returns(pizzas);

            var result = _sut.GetAllPizzas();

            var response = await result.ExecuteAsync(CancellationToken.None);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async void DeletePizza_ShouldReturnStatusCodeOkAndCallRemove()
        {
            var result = _sut.DeletePizza(int.MaxValue);

            var response = await result.ExecuteAsync(CancellationToken.None);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            A.CallTo(() => _pizzaService.Remove(A<Pizza>._)).MustHaveHappened();
        }
    }
}
