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
    public class OrdersResource : BaseResource
    {

        private readonly JsonDeserializer _jsonDeserializer;
        private const string ResourceName = "Ingredient";
        private const string ResourceNameWithParameter = ResourceName + "/{id}";

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

      
        [Test]
        public void Get_ShouldReturnStatusOk()
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
                Pizzas = new List<PizzaModel> { returnedPizzaModel }
            };
            request.AddJsonBody(order);

            var response = Client.Execute(request);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var getRequest = new RestRequest(ResourceName, Method.GET);
            var getResponse = Client.Execute(getRequest);

            Assert.That(getResponse, Is.Not.Null);
            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var orders = _jsonDeserializer.Deserialize<List<OrderModel>>(getResponse);
            CollectionAssert.IsNotEmpty(orders);
        }

        [Test]
        public void GetById_ShouldReturnStatusOk()
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
                Pizzas = new List<PizzaModel> { returnedPizzaModel }
            };
            request.AddJsonBody(order);

            var response = Client.Execute(request);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var orderModel = _jsonDeserializer.Deserialize<OrderModel>(response);

            var getRequest = new RestRequest(ResourceNameWithParameter, Method.GET);
            getRequest.AddUrlSegment("id", orderModel.Id.ToString());
            var getResponse = Client.Execute(getRequest);

            Assert.That(getResponse, Is.Not.Null);
            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var returnedOrderModel = _jsonDeserializer.Deserialize<IngredientModel>(getResponse);
            Assert.That(returnedOrderModel, Is.Not.Null);
            Assert.That(returnedOrderModel.Id, Is.EqualTo(orderModel.Id));
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
        public void Remove_ShouldReturnStatusOk()
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
                Pizzas = new List<PizzaModel> { returnedPizzaModel }
            };
            request.AddJsonBody(order);

            var response = Client.Execute(request);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var orderModel = _jsonDeserializer.Deserialize<OrderModel>(response);

            var deleteRequest = new RestRequest(ResourceNameWithParameter, Method.DELETE);
            deleteRequest.AddUrlSegment("id", orderModel.Id.ToString());
            var deleteResponse = Client.Execute(deleteRequest);

            Assert.That(deleteResponse, Is.Not.Null);
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void Update_ShouldReturnStatusOk()
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
                Pizzas = new List<PizzaModel> { returnedPizzaModel },
                Status = "Created"
            };
            request.AddJsonBody(order);

            var response = Client.Execute(request);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var orderModel = _jsonDeserializer.Deserialize<OrderModel>(response);

            order.Status = "InProgress";

            var putRequest = new RestRequest(ResourceNameWithParameter, Method.PUT);
            putRequest.AddUrlSegment("id", orderModel.Id.ToString());
            // putRequest.AddParameter("status", "Inprogress", ParameterType.UrlSegment);
            request.AddBody(order);
            var updateResponse = Client.Execute(putRequest);

         
            Assert.That(updateResponse, Is.Not.Null);
           // Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

    }
}
