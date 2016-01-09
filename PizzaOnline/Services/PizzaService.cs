using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaOnline.Model;
using PizzaOnline.Storage;

namespace PizzaOnline.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly IRepository<Pizza> _pizzasRepository;

        public PizzaService(IRepository<Pizza> pizzasRepository)
        {
            _pizzasRepository = pizzasRepository;
        }

        public Pizza Add(Pizza pizza)
        {
            if (pizza.Toppings == null || !pizza.Toppings.Any())
            {
                throw new ArgumentException("The pizza has to contain toppings.");
            }

            return _pizzasRepository.Persist(pizza);
        }

        public void Remove(Pizza pizza)
        {
            _pizzasRepository.Remove(pizza);
        }
    }
}
