using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaOnline.Model;

namespace PizzaOnline.Services
{
    public interface IOrderService
    {
        Order Add(Order order);
        IEnumerable<Order> GetOrders();
        Order Update(int id, string status);
        Order Get(int id);
    }
}
