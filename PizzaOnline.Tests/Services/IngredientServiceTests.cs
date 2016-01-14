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
        public void Add_ShouldPersistIngredientInRepository()
        {
            var ingredient = new Ingredient();

            _sut.Add(ingredient);

            A.CallTo(() => _ingredientsRepository.Persist(A<Ingredient>._))
                .MustHaveHappened();
        }

        [Test]
        public void Add_ShouldReturnIngredientFromRepository()
        {
            var expectedIngredient = new Ingredient();
            A.CallTo(() => _ingredientsRepository.Persist(A<Ingredient>._))
                .Returns(expectedIngredient);

            var result = _sut.Add(expectedIngredient);

            A.CallTo(() => _ingredientsRepository.Persist(A<Ingredient>._))
                .MustHaveHappened();
            Assert.That(result, Is.EqualTo(expectedIngredient));
        }

        [Test]
        public void Get_ShouldReturnIngredientFromRepository()
        {
            const int id = 123;

            _sut.Get(id);

            A.CallTo(() => _ingredientsRepository.FindById(id))
                .MustHaveHappened();
        }

        [Test]
        public void GetAll_ShouldReturnAllIngredientsFromRepository()
        {
            _sut.GetAll();

            A.CallTo(() => _ingredientsRepository.GetAll())
                .MustHaveHappened();
        }

        [Test]
        public void Remove_ShouldRemoveIngredientFromRepository()
        {
            var ingredient = new Ingredient();

            _sut.Remove(ingredient);

            A.CallTo(() => _ingredientsRepository.Remove(A<Ingredient>._))
                .MustHaveHappened();
        }

    }
}
