using System;
using System.Collections.Generic;

namespace PizzaOnline.Api.Models
{
    public class OrderModel
    {
        public int? Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }

        public IEnumerable<PizzaModel> Pizzas { get; set; }
        public IEnumerable<IngredientModel> Ingredients { get; set; }
    }
}