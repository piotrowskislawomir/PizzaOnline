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
        private readonly IPizzaRepository _pizzasRepository;

        public PizzaService(IPizzaRepository pizzasRepository)
        {
            _pizzasRepository = pizzasRepository;
        }

        public Pizza Add(Pizza pizza)
        {
            if (pizza.PizzasIngredients == null || !pizza.PizzasIngredients.Any())
            {
                throw new ArgumentException("The pizza has to contain toppings.");
            }

            var pizzaDb =  _pizzasRepository.Persist(pizza);
            if (pizzaDb != null && pizzaDb.Id.HasValue)
            {
                return Get(pizzaDb.Id.Value);
            }

            return null;
        }

        public void Remove(Pizza pizza)
        {
            _pizzasRepository.Remove(pizza);
        }

        public Pizza Get(int id)
        {
            return _pizzasRepository.GetByIdWithToppings(id);
        }

        public IEnumerable<Pizza> GetAllPizzas()
        {
            return _pizzasRepository.GetAllWithToppings();
        } 
      
   

       
    }
}
