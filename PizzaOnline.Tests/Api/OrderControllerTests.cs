using System;
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
        public async void GetOrder_ShouldReturnStatusCodeOk_WhenOrderWasFound()
        {

            
        }


    }
}
