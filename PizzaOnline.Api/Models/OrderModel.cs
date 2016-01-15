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

        public List<PizzaModel> Pizzas { get; set; }
        public List<IngredientModel> Ingredients { get; set; }
    }
}