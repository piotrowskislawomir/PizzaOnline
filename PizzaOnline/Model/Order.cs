using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOnline.Model
{
    public class Order : ModelBase
    {
        public DateTimeOffset CreationDate { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }

        public ICollection<OrdersPizzas> OrdersPizzas { get; set; }
        public ICollection<OrdersIngredients> OrdersIngredients { get; set; }
    }
}
