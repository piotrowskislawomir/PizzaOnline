using System;
using System.Collections.Generic;
using System.Data.Entity;
using NUnit.Framework;
using PizzaOnline.Model;
using PizzaOnline.Storage;

namespace PizzaOnline.Tests.Integration.Storage
{
    [TestFixture]
    public class PizzaRepositoryTests
    {
        [SetUp]
        public void SetUp()
        {
            _sut = new PizzaRepository(_dbContextFactory);
            _ingreditentsRepository = new Repository<Ingredient>(_dbContextFactory);
        }

        private static Func<DbContext> _dbContextFactory;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            _dbContextFactory = () => new PizzaOnlineContext("IntegrationTestsConnectionString");
            Database.SetInitializer(new DropCreateDatabaseAlways<PizzaOnlineContext>());
            _dbContextFactory().Database.Initialize(true);
        }


        private PizzaRepository _sut;
        private Repository<Ingredient> _ingreditentsRepository;

        [Test]
        public void GetByIdWithToppings_ShouldReturnPizzaWithIngredients_WhenIngredientsWereAdded()
        {
            var ingredient1 = new Ingredient{ Name = "ingredient1", Price = 10};
            var ingredient2 = new Ingredient {Name = "ingredient2", Price = 20};

            _ingreditentsRepository.Persist(ingredient1);
            _ingreditentsRepository.Persist(ingredient2);

            var expectedPizza = new Pizza
            {
                Name = "pizza1",
                Price = 123.12M,
                PizzasIngredients = new List<PizzasIngredients>()
                {
                    new PizzasIngredients {IngredientId = ingredient1.Id.Value},
                    new PizzasIngredients {IngredientId = ingredient2.Id.Value}
                }
            };

            var pizzaDb = _sut.Persist(expectedPizza);
            var result = _sut.GetByIdWithToppings(pizzaDb.Id.Value);

            Assert.That(result, Is.Not.Null);
            CollectionAssert.IsNotEmpty(result.PizzasIngredients);
            Assert.That(result.PizzasIngredients.Count, Is.EqualTo(expectedPizza.PizzasIngredients.Count));
        }


        [Test]
        public void GetByIdWithToppingsd_ShouldReturnEmptyCollectionOfIngredients_WhenIngredientsWereNotDefined()
        {
            var expectedPizza = new Pizza
            {
                Name = "pizza2",
                Price = 222M
            };

            var pizzaDb = _sut.Persist(expectedPizza);
            var result = _sut.GetByIdWithToppings(pizzaDb.Id.Value);

            Assert.That(result, Is.Not.Null);
            CollectionAssert.IsEmpty(result.PizzasIngredients);
        }

    }
}
