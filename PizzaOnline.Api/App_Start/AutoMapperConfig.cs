using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PizzaOnline.Api.Models;
using PizzaOnline.Model;

namespace PizzaOnline.Api
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<Ingredient, IngredientModel>();
            Mapper.CreateMap<IngredientModel, Ingredient>();

            Mapper.CreateMap<Pizza, PizzaModel>().ConstructUsing(s =>
                 new PizzaModel()
                 {
                     Name = s.Name,
                     Price = s.Price,
                     Toppings = s.PizzasIngredients
                     .Select(pi => Mapper.Map<IngredientModel>(pi.Ingredient)).ToList()
                 }
            );
            Mapper.CreateMap<PizzaModel, Pizza>().ConstructUsing(s =>
                 new Pizza()
                 {
                     Name = s.Name,
                     Price = s.Price,
                     PizzasIngredients = s.Toppings.Select(t => new PizzasIngredients()
                     {
                         IngredientId = t.Id.Value
                     }).ToList()
                 }
            );
        }
    }
    
}