using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaOnline.Model;
using PizzaOnline.Storage;

namespace PizzaOnline.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order Add(Order order)
        {
            if (order.OrdersPizzas == null || !order.OrdersPizzas.Any())
            {
                throw new ArgumentException("The order has to contain pizzas.");
            }

            var orderDb = _orderRepository.Persist(order);
            if (orderDb != null && orderDb.Id.HasValue)
            {
                return _orderRepository.FindById(orderDb.Id.Value);
            }

            return null;
        }
    }
}
