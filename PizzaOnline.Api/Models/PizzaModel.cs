using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaOnline.Api.Models
{
    public class PizzaModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<IngredientModel> Toppings { get; set; }
    }
}