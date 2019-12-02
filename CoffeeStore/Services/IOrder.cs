using CoffeeStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Services
{
    public interface IOrder
    {
        IEnumerable<Order> GetOrders { get; }
        Order GetOrder(int? Id);
        void Add(Order _Order);
        void Remove(int? Id);
    }
}
