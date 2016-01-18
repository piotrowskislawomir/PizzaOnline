using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PizzaOnline.Model;
using PizzaOnline.Storage;

namespace PizzaOnline.Tests.Integration.Storage
{
    [TestFixture]
    public class OrderRepositoryTests
    {

        /*
      

        [SetUp]
        public void SetUp()
        {
            _sut = new OrderRepository(_dbContextFactory);
            _ingreditentsRepository = new Repository<Ingredient>(_dbContextFactory);
            _pizzasRepository = new Repository<Pizza>(_dbContextFactory);
            _ordersRepository = new Repository<Order>(_dbContextFactory);

    }

    private static Func<DbContext> _dbContextFactory;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            _dbContextFactory = () => new PizzaOnlineContext("IntegrationTestsConnectionString");
            Database.SetInitializer(new DropCreateDatabaseAlways<PizzaOnlineContext>());
            _dbContextFactory().Database.Initialize(true);
        }

        private OrderRepository _sut;
        private Repository<Ingredient> _ingreditentsRepository;
        private Repository<Pizza> _pizzasRepository;
        private Repository<Order> _ordersRepository;

        
        [Test]
        public void GetById_ShouldReturnOrderWithPizzas_WhenIngredientsWereAdded()
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
                Status =  "Created",
                Price = 15.3M,
                OrdersPizzas = new List<OrdersPizzas>()
                {
                    new OrdersPizzas() {OrderId = pizza.Id.Value }
                }
            };



            var orderDb = _sut.Persist(expectedOrder);
            var result = _sut.GetOrders();

             Assert.That(result, Is.Not.Null);
             CollectionAssert.IsNotEmpty(result);
            Assert.That(result.Count, Is.EqualTo(expectedOrder));
        }

      */

    }
}
