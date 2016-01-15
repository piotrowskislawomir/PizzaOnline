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
        private IPizzaRepository _pizzasRepository;
        private PizzaService _sut;

        [SetUp]
        public void SetUp()
        {
            _pizzasRepository = A.Fake<IPizzaRepository>();
            _sut = new PizzaService(_pizzasRepository);
        }

        [Test]
        public void Add_ShouldPersistPizzaInRepository()
        {
            var pizza = new Pizza
            {
                PizzasIngredients = new List<PizzasIngredients>
                {
                    new PizzasIngredients(),
                    new PizzasIngredients()
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
                PizzasIngredients = new List<PizzasIngredients>()
            };

            Assert.Throws<ArgumentException>(() => _sut.Add(pizza));
        }

        [Test]
        public void Add_ShouldReturnNull_WhenPersistReturnedNull()
        {
            var pizza = new Pizza
            {
                PizzasIngredients = new List<PizzasIngredients>
                {
                    new PizzasIngredients(),
                    new PizzasIngredients()
                }
            };
            A.CallTo(() => _pizzasRepository.Persist(A<Pizza>._))
                .Returns(null);

            var result = _sut.Add(pizza);

            A.CallTo(() => _pizzasRepository.Persist(A<Pizza>._))
                .MustHaveHappened();
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ShouldReturnNotNullAndCallGet_WhenPersistReturnedNotNull()
        {
            var pizza = new Pizza
            {
                Id=1,
                PizzasIngredients = new List<PizzasIngredients>
                {
                    new PizzasIngredients(),
                    new PizzasIngredients()
                }
            };
            A.CallTo(() => _pizzasRepository.Persist(A<Pizza>._))
               .Returns(pizza);

            var result = _sut.Add(pizza);

            A.CallTo(() => _pizzasRepository.Persist(A<Pizza>._))
                .MustHaveHappened();
            Assert.That(result, Is.Not.Null);
            A.CallTo(() => _pizzasRepository.GetByIdWithToppings(A<int>._))
                .MustHaveHappened();
        }

        [Test]
        public void Remove_ShouldCallRemoveFromRepository()
        {
            var pizza = new Pizza();

            _sut.Remove(pizza);

            A.CallTo(() => _pizzasRepository.Remove(A<Pizza>._))
                .MustHaveHappened();
        }

        [Test]
        public void Get_ShouldCallGetFromRepository()
        {
            _sut.Get(int.MaxValue);

            A.CallTo(() => _pizzasRepository.GetByIdWithToppings(A<int>._)).
                MustHaveHappened();
        }

      
    }
}
