using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using FakeItEasy;
using NUnit.Framework;
using PizzaOnline.Api.Controllers;
using PizzaOnline.Api.Models;
using PizzaOnline.Model;
using PizzaOnline.Services;

namespace PizzaOnline.Tests.Api
{
    [TestFixture]
    public class IngredientControllerTests : BaseControllerTests
    {
        private IngredientController _sut;
        private IIngredientService _ingredientService;

        [SetUp]
        public void SetUp()
        {
            _ingredientService = A.Fake<IIngredientService>();
            _sut = new IngredientController(_ingredientService)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [Test]
        public async void GetIngredient_ShouldReturnStatusCodeNotFound_WhenTheIngredientWasNotFound()
        {
            A.CallTo(() => _ingredientService.Get(A<int>._)).Returns(null);

            var result = _sut.GetIngredient(int.MaxValue);

            var response = await result.ExecuteAsync(CancellationToken.None);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async void GetIngredient_ShouldReturnStatusCodeOk_WhenTheIngredientWasFound()
        {
            var ingredient = new Ingredient
            {
                Id = 2,
                Name = "ingredientName",
                Price = 123.2M
            };
            A.CallTo(() => _ingredientService.Get(A<int>._)).Returns(ingredient);

            var result = _sut.GetIngredient(int.MaxValue);

            var response = await result.ExecuteAsync(CancellationToken.None);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async void GetAllIngredients_ShouldReturnStatusCodeOk_WhenTheIngredientWasFound()
        {
            var ingredients = new List<Ingredient>
            {
                new Ingredient
                {
                    Id = 2,
                    Name = "ingredientName",
                    Price = 123.2M
                },
                new Ingredient
                {
                    Id = 3,
                    Name = "ingredientName4",
                    Price = 623.2M
                }
            };
            A.CallTo(() => _ingredientService.GetAll()).Returns(ingredients);

            var result = _sut.GetAllIngredients();

            var response = await result.ExecuteAsync(CancellationToken.None);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async void RemoveIngredient_ShouldReturnStatusCodeOkAndCallRemove()
        {
            var result = _sut.RemoveIngredient(int.MaxValue);

            var response = await result.ExecuteAsync(CancellationToken.None);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            A.CallTo(() => _ingredientService.Remove(A<Ingredient>._)).MustHaveHappened();
        }
    }
}
