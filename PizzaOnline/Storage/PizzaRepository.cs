using System;
using System.Data.Entity;
using System.Linq;
using PizzaOnline.Model;

namespace PizzaOnline.Storage
{
    public class PizzaRepository : Repository<Pizza>, IPizzaRepository
    {
        private readonly Func<DbContext> _contextFactory;

        public PizzaRepository(Func<DbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Pizza GetByIdWithToppings(int id)
        {
            using (var context = _contextFactory())
            {
                return context.Set<Pizza>()
                    .Include(p => p.PizzasIngredients.Select(i => i.Ingredient))
                    .FirstOrDefault(x => x.Id == id);
            }
        }
    }
}
