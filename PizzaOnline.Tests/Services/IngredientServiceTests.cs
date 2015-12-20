using FakeItEasy;
using NUnit.Framework;
using PizzaOnline.Model;
using PizzaOnline.Services;
using PizzaOnline.Storage;

namespace PizzaOnline.Tests.Services
{
    [TestFixture]
    public class IngredientServiceTests
    {
        private IngredientService _sut;
        private IRepository<Ingredient> _ingredientsRepository;

        [SetUp]
        public void SetUp()
        {
            _ingredientsRepository = A.Fake<IRepository<Ingredient>>();
            _sut = new IngredientService(_ingredientsRepository);
        }

        [Test]
        public void Add_Should_Persist_Ingredient_In_Repository()
        {
            var ingredient = new Ingredient();

            _sut.Add(ingredient);
            
            A.CallTo(() => _ingredientsRepository.Persist(A<Ingredient>._))
                .MustHaveHappened();
        }

    }
}
