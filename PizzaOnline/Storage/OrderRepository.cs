using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaOnline.Model;

namespace PizzaOnline.Storage
{
    public class OrderRepository :Repository<Order>, IOrderRepository
    {
        private readonly Func<DbContext> _contextFactory;

        public OrderRepository(Func<DbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Order GetById(int id)
        {
            using (var context = _contextFactory())
            {
                return context.Set<Order>()
                    .Include(o => o.OrdersIngredients.Select(i => i.Ingredient))
                    .Include(o => o.OrdersPizzas.Select(p => p.Pizza.PizzasIngredients.Select(i => i.Ingredient)))
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<Order> GetOrders()
        {
            using (var context = _contextFactory())
            {
                return context.Set<Order>()
                    .Include(o => o.OrdersIngredients.Select(i => i.Ingredient))
                    .Include(o => o.OrdersPizzas.Select(p => p.Pizza.PizzasIngredients.Select(i => i.Ingredient)))
                    .ToList();
            }
        }
    }
}
