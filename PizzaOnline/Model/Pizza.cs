using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOnline.Model
{
    public class Pizza : ModelBase
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<PizzasIngredients> PizzasIngredients { get; set; }
    }
}
