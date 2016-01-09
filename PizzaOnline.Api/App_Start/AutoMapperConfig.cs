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
            Mapper.CreateMap<IngredientModel, Ingredient>().ForMember(s => s.Id, a => a.Ignore());

            Mapper.CreateMap<Pizza, PizzaModel>();
            Mapper.CreateMap<PizzaModel, Pizza>().ForMember(s => s.Id, a => a.Ignore());
        }
    }
}