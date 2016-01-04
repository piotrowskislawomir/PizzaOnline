using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaOnline.Api.Models
{
    public class IngredientModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}