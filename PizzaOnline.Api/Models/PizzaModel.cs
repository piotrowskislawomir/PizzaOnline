using System.Collections.Generic;

namespace PizzaOnline.Api.Models
{
    public class PizzaModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<IngredientModel> Toppings { get; set; }
    }
}