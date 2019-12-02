using CoffeeStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Services
{
    public interface IOrderDetail
    {
        IEnumerable<OrderDetail> GetOrderDetails { get; }
        OrderDetail GetOrderDetail(int? Id);
        void Add(OrderDetail _OrderDetail);
        void Remove(int? Id);
    }
}
