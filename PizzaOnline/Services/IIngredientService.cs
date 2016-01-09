using System.Collections;
using System.Collections.Generic;
using PizzaOnline.Model;

namespace PizzaOnline.Services
{
    public interface IIngredientService
    {
        Ingredient Add(Ingredient ingredient);
        Ingredient Get(int id);
        IEnumerable<Ingredient> GetAll();
    }
}
