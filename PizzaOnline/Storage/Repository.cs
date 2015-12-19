using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PizzaOnline.Model;

namespace PizzaOnline.Storage
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly Func<DbContext> _contextFactory;

        public Repository(Func<DbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public T FindById(int id)
        {
            using (var context = _contextFactory())
            {
                return context.Set<T>().FirstOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var context = _contextFactory())
            {
                return context.Set<T>().AsEnumerable().ToList();
            }
        }

        public T Persist(T item)
        {
            using (var context = _contextFactory())
            {
                if (!item.Id.HasValue)
                {
                    context.Set<T>().Add(item);
                }
                else
                {
                    var original = context.Set<T>().Single(x => x.Id == item.Id);
                    context.Entry(original).CurrentValues.SetValues(item);
                }
                context.SaveChanges();
            }

            if (item.Id.HasValue)
            {
                return FindById(item.Id.Value);
            }

            return null;

        }

        public void Remove(T item)
        {
            using (var context = _contextFactory())
            {
                context.Set<T>().Remove(context.Set<T>().Single(x => x.Id == item.Id));
                context.SaveChanges();
            }
        }
    }
}
