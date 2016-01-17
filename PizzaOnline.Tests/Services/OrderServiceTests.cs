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
    public class OrderServiceTests
    {
        private IOrderRepository _orderRepository;
        private OrderService _sut;

        [SetUp]
        public void SetUp()
        {
            _orderRepository = A.Fake<IOrderRepository>();
            _sut = new OrderService(_orderRepository);
        }

        [Test]
        public void Add_ShouldPersistPizzaInRepository()
        {
            var order = new Order()
            {
                OrdersPizzas = new List<OrdersPizzas>
                {
                    new OrdersPizzas()
                }
            };

            _sut.Add(order);

            A.CallTo(() => _orderRepository.Persist(A<Order>._))
                .MustHaveHappened();
        }

        [Test]
        public void Add_ShouldOccurException_WhenCollectionOfToppingsIsNull()
        {
            var order = new Order();

            Assert.Throws<ArgumentException>(() => _sut.Add(order));
        }

        [Test]
        public void Add_ShouldOccurException_WhenCollectionOfToppingsIsEmpty()
        {
            var order = new Order
            {
                OrdersPizzas = new List<OrdersPizzas>()
            };

            Assert.Throws<ArgumentException>(() => _sut.Add(order));
        }

        [Test]
        public void Add_ShouldReturnNull_WhenPersistReturnedNull()
        {
            var order = new Order()
            {
                OrdersPizzas = new List<OrdersPizzas>
                {
                    new OrdersPizzas()
                }
            };

            A.CallTo(() => _orderRepository.Persist(A<Order>._))
                .Returns(null);

            var result = _sut.Add(order);

            A.CallTo(() => _orderRepository.Persist(A<Order>._))
                .MustHaveHappened();
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ShouldReturnNotNullAndCallGet_WhenPersistReturnedNotNull()
        {
            var order = new Order()
            {
                Id = 1,
                OrdersPizzas = new List<OrdersPizzas>
                {
                    new OrdersPizzas()
                }
            };
            A.CallTo(() => _orderRepository.Persist(A<Order>._))
                .Returns(order);

            var result = _sut.Add(order);

            A.CallTo(() => _orderRepository.Persist(A<Order>._))
                .MustHaveHappened();
            Assert.That(result, Is.Not.Null);
            A.CallTo(() => _orderRepository.FindById(A<int>._))
                .MustHaveHappened();
        }

        [Test]
        public void GetOrders_ShouldCallGetAllOrders()
        {
            _sut.GetOrders();

            A.CallTo(() => _orderRepository.GetOrders())
                .MustHaveHappened();
        }

      

    }
}
