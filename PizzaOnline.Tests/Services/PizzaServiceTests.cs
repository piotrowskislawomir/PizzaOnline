using System;
using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using PizzaOnline.Model;
using PizzaOnline.Services;
using PizzaOnline.Storage;

namespace PizzaOnline.Tests.Services
{
    [TestFixture]
    public class PizzaServiceTests
    {
        private IRepository<Pizza> _pizzasRepository;
        private PizzaService _sut;

        [SetUp]
        public void SetUp()
        {
            _pizzasRepository = A.Fake<IRepository<Pizza>>();
            _sut = new PizzaService(_pizzasRepository);
        }

        [Test]
        public void Add_ShouldPersistPizzaInRepository()
        {
            var pizza = new Pizza
            {
                Toppings = new List<Ingredient>
                {
                    new Ingredient(),
                    new Ingredient()
                }
            };

            _sut.Add(pizza);

            A.CallTo(() => _pizzasRepository.Persist(A<Pizza>._))
                .MustHaveHappened();
        }

        [Test]
        public void Add_ShouldOccurException_WhenCollectionOfToppingsIsNull()
        {
            var pizza = new Pizza();

            Assert.Throws<ArgumentException>(() => _sut.Add(pizza));
        }

        [Test]
        public void Add_ShouldOccurException_WhenCollectionOfToppingsIsEmpty()
        {
            var pizza = new Pizza
            {
                Toppings = new List<Ingredient>()
            };

            Assert.Throws<ArgumentException>(() => _sut.Add(pizza));
        }
    }
}
