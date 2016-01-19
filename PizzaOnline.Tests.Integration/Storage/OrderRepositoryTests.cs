using System.Collections.Generic;
using NUnit.Framework;
using PizzaOnline.Model;
using PizzaOnline.Storage;

namespace PizzaOnline.Tests.Integration.Storage
{
    [TestFixture]
    public class OrderRepositoryTests :BaseRepositoryTests
    {
        private OrderRepository _sut;
        private Repository<Ingredient> _ingreditentsRepository;
        private Repository<Pizza> _pizzasRepository;

        [SetUp]
        public void SetUp()
        {
            _sut = new OrderRepository(DbContextFactory);
            _ingreditentsRepository = new Repository<Ingredient>(DbContextFactory);
            _pizzasRepository = new Repository<Pizza>(DbContextFactory);
        }
        
        [Test]
        public void GetOrders_ShouldReturnOrderWithPizzas_WheOrderWasAdded()
        {
            var ingredient1 = new Ingredient {Name = "ingredient1", Price = 10};
            var ingredient2 = new Ingredient {Name = "ingredient2", Price = 20};

            _ingreditentsRepository.Persist(ingredient1);
            _ingreditentsRepository.Persist(ingredient2);

            var pizza = new Pizza
            {
                Name = "pizza1",
                Price = 123.12M,
                PizzasIngredients = new List<PizzasIngredients>()
                {
                    new PizzasIngredients {IngredientId = ingredient1.Id.Value},
                    new PizzasIngredients {IngredientId = ingredient2.Id.Value}
                }
            };

            _pizzasRepository.Persist(pizza);

            var expectedOrder = new Order
            {
                Address = "Warszawa",
                Price = 15.3M,
                OrdersPizzas = new List<OrdersPizzas>()
                {
                    new OrdersPizzas() {PizzaId = pizza.Id.Value}
                }
            };

            var orderDb = _sut.Persist(expectedOrder);
            Assert.That(orderDb, Is.Not.Null);


            var result = _sut.GetById(orderDb.Id.Value);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Address, Is.EqualTo(expectedOrder.Address));
        }

        [Test]
        public void GetOrders_ShouldReturnCollectionOfOrders_WheOrdersWereAdded()
        {
            var ingredient1 = new Ingredient { Name = "ingredient1", Price = 10 };
            var ingredient2 = new Ingredient { Name = "ingredient2", Price = 20 };

            _ingreditentsRepository.Persist(ingredient1);
            _ingreditentsRepository.Persist(ingredient2);

            var pizza = new Pizza
            {
                Name = "pizza1",
                Price = 123.12M,
                PizzasIngredients = new List<PizzasIngredients>()
                {
                    new PizzasIngredients {IngredientId = ingredient1.Id.Value},
                    new PizzasIngredients {IngredientId = ingredient2.Id.Value}
                }
            };

            _pizzasRepository.Persist(pizza);

            var expectedOrder = new Order
            {
                Address = "Warszawa",
                Price = 15.3M,
                OrdersPizzas = new List<OrdersPizzas>()
                {
                    new OrdersPizzas() {PizzaId = pizza.Id.Value}
                }
            };

            var orderDb = _sut.Persist(expectedOrder);
            Assert.That(orderDb, Is.Not.Null);


            var result = _sut.GetAll();

            Assert.That(result, Is.Not.Null);
            CollectionAssert.IsNotEmpty(result);
        }
    }
}
