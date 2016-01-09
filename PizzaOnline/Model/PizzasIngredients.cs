using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOnline.Model
{
    public class PizzasIngredients
    {
        [Key, Column(Order = 0)]
        public int PizzaId { get; set; }
        [Key, Column(Order = 1)]
        public int IngredientId { get; set; }

        public virtual Pizza Pizza { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        
    }
}
