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
        /*
        [Test]
        public void Post_ShouldReturnStatusCodeCreatedNewPizza()
        {
            var request = new RestRequest(ResourceName, Method.POST);

            var ingredient = new Ingredient() {Name = "pieczarka", Price = 2};

            var pizza = new Pizza
            {
                Name = "Z szynką",
                Price = 10,
                PizzasIngredients = 
            };

            request.AddJsonBody(pizza);

            var response = Client.Execute(request);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }*/

    }
}
