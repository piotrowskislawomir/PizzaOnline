using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PizzaOnline.Api.Models;
using PizzaOnline.Model;
using PizzaOnline.Services;

namespace PizzaOnline.Api.Controllers
{
    public class PizzaController : ApiController
    {
        private readonly IPizzaService _pizzaService;

        public PizzaController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [Route("api/pizza/{id}")]
        [HttpDelete]
        public IHttpActionResult DeletePizza(int id)
        {
            var pizza = new Pizza {Id = id};
            _pizzaService.Remove(pizza);
            return Ok();
        }

        [Route("api/pizza")]
        [HttpPost]
        public IHttpActionResult CreateIngredient(PizzaModel ingredientModel)
        {
            var ingredient = Mapper.Map<Pizza>(ingredientModel);

            var ingredientDb = _pizzaService.Add(ingredient);

            return CreatedAtRoute("GetPizzaById",
                new { id = ingredient.Id },
                Mapper.Map<IngredientModel>(ingredientDb));
        }
    }
}
