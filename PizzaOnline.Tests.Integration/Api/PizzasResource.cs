using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PizzaOnline.Api.Models;
using PizzaOnline.Model;
using RestSharp;
using RestSharp.Deserializers;

namespace PizzaOnline.Tests.Integration.Api
{

    [Ignore]
    [TestFixture]
    class PizzasResource : BaseResource
    {
        private readonly JsonDeserializer _jsonDeserializer;
        private const string ResourceName = "Pizza";
        private const string ResourceNameWithParameter = ResourceName + "/{id}";

        public PizzasResource()
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

            var pizzaRequest = new RestRequest(ResourceName, Method.POST);
            var pizza = new PizzaModel
            {
                Name = "pizza1",
                Price = 123,
                Toppings = new List<IngredientModel> {returnedIngredientModel}
            };
            pizzaRequest.AddJsonBody(pizza);

            var pizzaResponse = Client.Execute(pizzaRequest);

            Assert.That(pizzaResponse, Is.Not.Null);
            Assert.That(pizzaResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
           
        }

        [Test]
        public void Delete_ShouldReturnStatusCodeOk()
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

            var pizzaRequest = new RestRequest(ResourceName, Method.POST);
            var pizza = new PizzaModel
            {
                Name = "pizza1",
                Price = 123,
                Toppings = new List<IngredientModel> {returnedIngredientModel}
            };
            pizzaRequest.AddJsonBody(pizza);

            var pizzaResponse = Client.Execute(pizzaRequest);

            Assert.That(pizzaResponse, Is.Not.Null);
            Assert.That(pizzaResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var pizzaModel = _jsonDeserializer.Deserialize<PizzaModel>(pizzaResponse);

            var deleteRequest = new RestRequest(ResourceNameWithParameter, Method.DELETE);
            deleteRequest.AddUrlSegment("id", pizzaModel.Id.ToString());
            var deleteResponse = Client.Execute(deleteRequest);

            Assert.That(deleteResponse, Is.Not.Null);
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void GetById_ShouldReturnStatusCodeOk()
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

            var pizzaRequest = new RestRequest(ResourceName, Method.POST);
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

            var pizzaModel = _jsonDeserializer.Deserialize<PizzaModel>(pizzaResponse);

            var getRequest = new RestRequest(ResourceNameWithParameter, Method.GET);
            getRequest.AddUrlSegment("id", pizzaModel.Id.ToString());
            var getResponse = Client.Execute(getRequest);

            Assert.That(getResponse, Is.Not.Null);
            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var returnedPizzaModel = _jsonDeserializer.Deserialize<IngredientModel>(getResponse);
            Assert.That(returnedPizzaModel, Is.Not.Null);
            Assert.That(returnedPizzaModel.Id, Is.EqualTo(pizzaModel.Id));
        }

        [Test]
        public void GetById_ShouldReturnStatusNotFound()
        {
                        
            var getRequest = new RestRequest(ResourceNameWithParameter, Method.GET);
            getRequest.AddUrlSegment("id", int.MaxValue.ToString());
            var getResponse = Client.Execute(getRequest);

            Assert.That(getResponse, Is.Not.Null);
            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
           
        }

        [Test]
        public void Get_ShouldReturnStatusCodeOk()
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

            var pizzaRequest = new RestRequest(ResourceName, Method.POST);
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

            var getRequest = new RestRequest(ResourceName, Method.GET);
            var getResponse = Client.Execute(getRequest);

            Assert.That(getResponse, Is.Not.Null);
            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var returnedPizzas = _jsonDeserializer.Deserialize<List<PizzaModel>>(getResponse);
            CollectionAssert.IsNotEmpty((returnedPizzas));
        }
    }
}