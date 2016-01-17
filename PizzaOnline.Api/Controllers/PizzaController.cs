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

        [Route("api/pizza/{id}", Name = "GetPizzaById")]
        [HttpGet]
        public IHttpActionResult GetPizza(int id)
        {
            var pizza = _pizzaService.Get(id);

            if (pizza != null)
            {
                return Ok(Mapper.Map<PizzaModel>(pizza));
            }

            return NotFound();
        }

        [Route("api/pizza")]
        [HttpPost]
        public IHttpActionResult CreatePizza(PizzaModel pizzaModel)
        {
            var pizza = Mapper.Map<Pizza>(pizzaModel);

            var pizzaDb = _pizzaService.Add(pizza);

            return CreatedAtRoute("GetPizzaById",
                new { id = pizzaDb.Id },
                Mapper.Map<PizzaModel>(pizzaDb));
        }

        [Route("api/pizza")]
        [HttpGet]
        public IHttpActionResult GetAllPizzas()
        {
            var pizzas = _pizzaService.GetAllPizzas();
            return Ok(pizzas.Select(Mapper.Map<PizzaModel>));
        }


    }
}
