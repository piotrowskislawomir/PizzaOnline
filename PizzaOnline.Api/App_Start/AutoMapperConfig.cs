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
                     Toppings = s.PizzasIngredients?.Select(pi => Mapper.Map<IngredientModel>(pi.Ingredient)).ToList()
                 }
            );
            Mapper.CreateMap<PizzaModel, Pizza>().ConstructUsing(s =>
                 new Pizza()
                 {
                     Name = s.Name,
                     Price = s.Price,
                     PizzasIngredients = s.Toppings?.Select(t => new PizzasIngredients()
                     {
                         IngredientId = t.Id.Value
                     }).ToList()
                 }
            );

            Mapper.CreateMap<Order, OrderModel>().ConstructUsing(s =>
                 new OrderModel()
                 {
                     CreationDate = s.CreationDate,
                     Price = s.Price,
                     Status = s.Status,
                     Address = s.Address,
                     Pizzas = s.OrdersPizzas?
                     .Select(pi => Mapper.Map<PizzaModel>(pi.Pizza)).ToList(),
                     Ingredients = s.OrdersIngredients?
                     .Select(pi => Mapper.Map<IngredientModel>(pi.Ingredient)).ToList()
                 }
            );
            Mapper.CreateMap<OrderModel, Order>().ConstructUsing(s =>
                 new Order()
                 {
                     CreationDate = s.CreationDate,
                     Price = s.Price,
                     Address = s.Address,
                     Status = s.Status,
                     OrdersIngredients = s.Ingredients?.Select(t => new OrdersIngredients()
                     {
                         IngredientId = t.Id.Value
                     }).ToList(),
                     OrdersPizzas = s.Pizzas?.Select(t => new OrdersPizzas()
                     {
                         PizzaId = t.Id.Value
                     }).ToList()
                 }
            );
        }
    }
    
}