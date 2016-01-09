using System.Collections;
using System.Collections.Generic;

namespace PizzaOnline.Model
{
    public class Ingredient : ModelBase
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<PizzasIngredients> PizzasIngregients { get; set; }
    }
}
