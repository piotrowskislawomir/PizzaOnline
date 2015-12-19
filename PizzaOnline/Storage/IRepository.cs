using System.Collections.Generic;
using PizzaOnline.Model;

namespace PizzaOnline.Storage
{
    public interface IRepository<T> where T : class, IEntity
    {
        IEnumerable<T> GetAll();

        T FindById(int id);

        T Persist(T item);

        void Remove(T item);
    }
}
