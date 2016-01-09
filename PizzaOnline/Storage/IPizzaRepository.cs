using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaOnline.Model;

namespace PizzaOnline.Storage
{
    public interface IPizzaRepository
    {
        Pizza Persist(Pizza item);
        void Remove(Pizza item);
        Pizza GetByIdWithToppings(int id);
    }
}
