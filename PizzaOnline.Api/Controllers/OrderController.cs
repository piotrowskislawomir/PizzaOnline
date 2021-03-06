﻿using System;
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
        public IHttpActionResult GetOrder(int id)
        {
            var order = _orderService.Get(id);

            if(order != null)
            {
                return Ok(Mapper.Map<OrderModel>(order));
            }

            return NotFound();
        }

        [Route("api/order")]
        [HttpPost]
        public IHttpActionResult CreateOrder(OrderModel orderModel)
        {
            var order = Mapper.Map<Order>(orderModel);

            var orderDb = _orderService.Add(order);

            return CreatedAtRoute("GetOrderById",
                new { id = orderDb.Id },
                Mapper.Map<OrderModel>(orderDb));
        }

        [Route("api/order/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateOrder(int id, OrderModel orderModel)
        {
            var orderDb = _orderService.Update(id, orderModel.Status);

            return Ok(Mapper.Map<OrderModel>(orderDb));
        }

        [Route("api/order")]
        [HttpGet]
        public IHttpActionResult GetOrders()
        {
            var orders = _orderService.GetOrders();

            return Ok(orders.Select(Mapper.Map<OrderModel>));
        }
    }
}