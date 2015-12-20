using PizzaOnline.Model;
using PizzaOnline.Storage;

namespace PizzaOnline.Services
{
    public class IngredientService :IIngredientService
    {
        private readonly IRepository<Ingredient> _ingredinetRepository;
        public IngredientService(IRepository<Ingredient> ingredinetRepository)
        {
            _ingredinetRepository = ingredinetRepository;
        }

        public Ingredient Add(Ingredient ingredient)
        {
            return _ingredinetRepository.Persist(ingredient);
        }
    }
}
