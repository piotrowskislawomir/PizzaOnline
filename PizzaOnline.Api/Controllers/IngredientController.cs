﻿using System;
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
    public class IngredientController : ApiController
    {
        private readonly IIngredientService _ingredientService;
        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [Route("api/ingredient")]
        [HttpPost]
        public IHttpActionResult CreateIngredient(IngredientModel ingredientModel)
        {
            var ingredient = Mapper.Map<Ingredient>(ingredientModel);

            var ingredientDb = _ingredientService.Add(ingredient);
            
            return CreatedAtRoute("GetIngredientById",
                new {id = ingredient.Id},
                Mapper.Map<IngredientModel>(ingredientDb));
        }

        [Route("api/ingredient/{id}", Name = "GetIngredientById")]
        [HttpGet]
        public IHttpActionResult GetIngredient(int id)
        {
            var ingredient = _ingredientService.Get(id);
            return Ok(Mapper.Map<IngredientModel>(ingredient));
        }

        [Route("api/ingredients")]
        [HttpGet]
        public IHttpActionResult GetAllIngredients()
        {
            var ingredients = _ingredientService.GetAll();
            return Ok(ingredients.Select(Mapper.Map<IngredientModel>));
        }
    }
}
