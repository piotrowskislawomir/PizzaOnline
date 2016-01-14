using PizzaOnline.Model;

namespace PizzaOnline.Storage
{
    public interface IPizzaRepository: IRepository<Pizza>
    {
        Pizza GetByIdWithToppings(int id);
    }
}
