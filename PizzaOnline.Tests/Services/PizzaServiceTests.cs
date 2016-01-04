using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var pizza = new Pizza();

            _sut.Add(pizza);

            A.CallTo(() => _pizzasRepository.Persist(A<Pizza>._))
                .MustHaveHappened();
        }
    }
}
