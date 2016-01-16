using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaOnline.Model;

namespace PizzaOnline.Services
{
    public interface IPizzaService
    {
        Pizza Add(Pizza pizza);
        void Remove(Pizza pizza);
        Pizza Get(int id);
        IEnumerable<Pizza> GetAllPizzas();

    }
}
