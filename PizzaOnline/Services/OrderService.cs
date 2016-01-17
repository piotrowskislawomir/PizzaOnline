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

            order.Status = OrderStatuses.Created.ToString();
            order.CreationDate = DateTimeOffset.Now;

            var orderDb = _orderRepository.Persist(order);
            if (orderDb != null && orderDb.Id.HasValue)
            {
                return _orderRepository.GetById(orderDb.Id.Value);
            }

            return null;
        }

        public IEnumerable<Order> GetOrders()
        {
            return _orderRepository.GetOrders();
        }

        public Order Update(int id, string status)
        {
            var orderDb = _orderRepository.GetById(id);

            if (orderDb != null)
            {
                orderDb.Status = status;
                return _orderRepository.Persist(orderDb);
            }

            return null;
        }
    }
}
