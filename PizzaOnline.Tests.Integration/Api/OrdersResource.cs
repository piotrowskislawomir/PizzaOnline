using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PizzaOnline.Api.Models;
using RestSharp;
using RestSharp.Deserializers;

namespace PizzaOnline.Tests.Integration.Api
{
    [Ignore]
    [TestFixture]
    public class OrdersResource : BaseResource
    {

        private readonly JsonDeserializer _jsonDeserializer;
        private const string ResourceName = "Ingredient";

        public OrdersResource()
        {
            _jsonDeserializer = new JsonDeserializer();
        }

        [Test]
        public void Post_ShouldReturnStatusCodeCreated()
        {
            var ingredientRequest = new RestRequest("Ingredient", Method.POST);
            var ingredient = new IngredientModel
            {
                Name = "ingredient1",
                Price = 123
            };
            ingredientRequest.AddJsonBody(ingredient);

            var ingredientResponse = Client.Execute(ingredientRequest);

            Assert.That(ingredientResponse, Is.Not.Null);
            Assert.That(ingredientResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            var returnedIngredientModel = _jsonDeserializer.Deserialize<IngredientModel>(ingredientResponse);

            var pizzaRequest = new RestRequest("Pizza", Method.POST);
            var pizza = new PizzaModel
            {
                Name = "pizza1",
                Price = 123,
                Toppings = new List<IngredientModel> { returnedIngredientModel }
            };
            pizzaRequest.AddJsonBody(pizza);

            var pizzaResponse = Client.Execute(pizzaRequest);

            Assert.That(pizzaResponse, Is.Not.Null);
            Assert.That(pizzaResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            var returnedPizzaModel = _jsonDeserializer.Deserialize<PizzaModel>(pizzaResponse);
            
            var request = new RestRequest(ResourceName, Method.POST);
            var order = new OrderModel
            {
                CreationDate = DateTimeOffset.Now,
                Price = 123,
                Address = "address",
                Pizzas = new List<PizzaModel> { returnedPizzaModel}
            };
            request.AddJsonBody(order);

            var response = Client.Execute(request);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

    }
}
