using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using FakeItEasy;
using NUnit.Framework;
using PizzaOnline.Api.Controllers;
using PizzaOnline.Api.Models;
using PizzaOnline.Model;
using PizzaOnline.Services;

namespace PizzaOnline.Tests.Api
{
    [TestFixture]
    public class OrderControllerTests : BaseControllerTests
    {
        private OrderController _sut;
        private IOrderService _orderService;

        [SetUp]
        public void SetUp()
        {
            _orderService = A.Fake<IOrderService>();
            _sut = new OrderController(_orderService)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [Test]
        public async void GetOrder_ShouldReturnStatusCodeNotFound_WhenTheOrderWasNotFound()
        {
            A.CallTo(() => _orderService.Get(A<int>._)).Returns(null);

            var result = _sut.GetOrder(int.MaxValue);

            var response = await result.ExecuteAsync(CancellationToken.None);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }


        [Test]
        public async void GetOrder_ShouldReturnStatusCodeOk_WhenTheOrderWasFound()
        {
            var order = new Order
            {
                Id = 1,
                Address = "Poznań",
                Price = 10.2M,
                Status = "InProgress"
            };

            A.CallTo(() => _orderService.Get(A<int>._)).Returns(order);

            var result = _sut.GetOrder(int.MaxValue);

            var response = await result.ExecuteAsync(CancellationToken.None);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }


        [Test]
        public async void GetOrders_ShouldReturnStatusCodeOk_WhenOrderWasFound()
        {
            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    Address = "Poznań",
                    Price = 10.2M,
                    Status = "InProgress"
                },
                new Order
                {
                    Id = 1,
                    Address = "Warszawa",
                    Price = 10.7M,
                    Status = "InProgress"
                }

            };

            A.CallTo(() => _orderService.GetOrders()).Returns(orders);

            var result = _sut.GetOrders();

            var response = await result.ExecuteAsync(CancellationToken.None);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

      
        [Test]
        public async void PutOrder_ShouldReturnStatusCodeOk_WhenOrderWasUpdate()
        {
                var order = new Order
            {
                Id = 1,
                Address = "Poznań",
                Price = 10.2M,
                Status = "Created"
            };

            var orderModel = new OrderModel
            {
                Id = 1,
                Address = "Poznań",
                Price = 10.2M,
                Status = "InProgress"
            };
        
            A.CallTo(() => _orderService.Update(int.MaxValue, "InProgress")).Returns(order);

            var result = _sut.UpdateOrder(int.MaxValue, orderModel);

            var response = await result.ExecuteAsync(CancellationToken.None);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        






    }
}
