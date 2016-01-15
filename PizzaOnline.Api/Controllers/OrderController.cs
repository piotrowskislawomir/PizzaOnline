using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using PizzaOnline.Api.Models;
using PizzaOnline.Model;
using PizzaOnline.Services;

namespace PizzaOnline.Api.Controllers
{
    public class OrderController :ApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [Route("api/order/{id}", Name = "GetOrderById")]
        [HttpGet]
        public IHttpActionResult GetPizza(int id)
        {
            return Ok();
        }

        [Route("api/order")]
        [HttpPost]
        public IHttpActionResult CreatePizza(OrderModel orderModel)
        {
            var order = Mapper.Map<Order>(orderModel);

            var orderDb = _orderService.Add(order);

            return CreatedAtRoute("GetOrderById",
                new { id = orderDb.Id },
                Mapper.Map<OrderModel>(orderDb));
        }
    }
}