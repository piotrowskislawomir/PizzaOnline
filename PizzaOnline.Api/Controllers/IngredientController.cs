using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PizzaOnline.Api.Models;
using PizzaOnline.Model;
using PizzaOnline.Services;

namespace PizzaOnline.Api.Controllers
{
    public class IngredientController : ApiController
    {
        private readonly IIngredientService _ingredientService;
        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [Route("api/ingredient")]
        [HttpPost]
        public IHttpActionResult CreateIngredient(IngredientModel request)
        {
            var model = new Ingredient()
            {
                Name = request.Name,
                Price = request.Price
            };

            var ingredient = _ingredientService.Add(model);

            var ingredientModel = new IngredientModel
            {
                Id = ingredient.Id.Value,
                Name = ingredient.Name,
                Price = ingredient.Price
            };

            return CreatedAtRoute("GetIngredientById", new {id = ingredient.Id}, ingredientModel);
        }

        [Route("api/ingredient{id}", Name = "GetIngredientById")]
        public IHttpActionResult GetIngredient(int id)
        {
            return Ok();
        }
    }
}
