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
    }
}
