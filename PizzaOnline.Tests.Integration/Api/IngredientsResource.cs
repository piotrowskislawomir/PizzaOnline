using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using PizzaOnline.Api.Models;
using RestSharp;
using RestSharp.Deserializers;

namespace PizzaOnline.Tests.Integration.Api
{
    [Ignore]
    [TestFixture]
    public class IngredientsResource : BaseResource
    {
        private readonly JsonDeserializer _jsonDeserializer;
        private const string ResourceName = "Ingredient";
        private const string ResourceNameWithParameter = ResourceName + "/{id}";

        public IngredientsResource()
        {
            _jsonDeserializer = new JsonDeserializer();
        }

        [Test]
        public void Post_ShouldReturnStatusCodeCreated()
        {
            var request = new RestRequest(ResourceName, Method.POST);
            var ingredient = new IngredientModel
            {
                Name = "ingredient1",
                Price = 123
            };
            request.AddJsonBody(ingredient);

            var response = Client.Execute(request);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void Delete_ShouldReturnStatusCodeOk()
        {
            var postRequest = new RestRequest(ResourceName, Method.POST);
            var ingredient = new IngredientModel
            {
                Name = "ingredient1",
                Price = 123
            };
            postRequest.AddJsonBody(ingredient);

            var postResponse = Client.Execute(postRequest);
            Assert.That(postResponse, Is.Not.Null);
            Assert.That(postResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            
            var ingredientModel = _jsonDeserializer.Deserialize<IngredientModel>(postResponse);

            var deleteRequest = new RestRequest(ResourceNameWithParameter, Method.DELETE);
            deleteRequest.AddUrlSegment("id", ingredientModel.Id.ToString());
            var deleteResponse = Client.Execute(deleteRequest);

            Assert.That(deleteResponse, Is.Not.Null);
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }


        [Test]
        public void GetById_ShouldReturnStatusCodeOk()
        {
            var postRequest = new RestRequest(ResourceName, Method.POST);
            var expectedIngredient = new IngredientModel
            {
                Name = "ingredient1",
                Price = 123
            };
            postRequest.AddJsonBody(expectedIngredient);

            var postResponse = Client.Execute(postRequest);
            Assert.That(postResponse, Is.Not.Null);
            Assert.That(postResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var ingredientModel = _jsonDeserializer.Deserialize<IngredientModel>(postResponse);

            var getRequest = new RestRequest(ResourceNameWithParameter, Method.GET);
            getRequest.AddUrlSegment("id", ingredientModel.Id.ToString());
            var getResponse = Client.Execute(getRequest);

            Assert.That(getResponse, Is.Not.Null);
            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var returnedIngredientModel = _jsonDeserializer.Deserialize<IngredientModel>(getResponse);
            Assert.That(returnedIngredientModel, Is.Not.Null);
            Assert.That(returnedIngredientModel.Id, Is.EqualTo(ingredientModel.Id));
        }

        [Test]
        public void GetById_ShouldReturnStatusCodeNotFound()
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
            var postRequest = new RestRequest(ResourceName, Method.POST);
            var expectedIngredient = new IngredientModel
            {
                Name = "ingredient1",
                Price = 123
            };
            postRequest.AddJsonBody(expectedIngredient);

            var postResponse = Client.Execute(postRequest);
            Assert.That(postResponse, Is.Not.Null);
            Assert.That(postResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var getRequest = new RestRequest(ResourceName, Method.GET);
            var getResponse = Client.Execute(getRequest);

            Assert.That(getResponse, Is.Not.Null);
            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var ingredients = _jsonDeserializer.Deserialize<List<IngredientModel>>(getResponse);
            CollectionAssert.IsNotEmpty(ingredients);
        }
    }
}
