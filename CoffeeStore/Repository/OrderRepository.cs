using CoffeeStore.Models;
using CoffeeStore.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Repository
{
    public class OrderRepository:IOrder
    {
        private readonly DB_Context db;
        public OrderRepository(DB_Context _db)
        {
            db = _db;
        }
        public IEnumerable<Order> GetOrders => db.Orders.Include(e=>e.Customers);
        public Order GetOrder(int? Id)
        {
            Order dbEntity = db.Orders.Include(o=>o.Customers)
                                      .Include(o=>o.OrderDetails)
                                      .ThenInclude(o=>o.Items)
                                      .SingleOrDefault(o=>o.orderID==Id);
            return dbEntity;
        }
        public void Add(Order _Order)
        {
            if(_Order.orderID==0)
            {
                db.Orders.Add(_Order);
                db.SaveChanges();
            }
            else
            {
                db.Orders.Update(_Order);
                db.SaveChanges();
            }           
        }
        public void Remove(int? Id)
        {
            Order dbEntity = db.Orders.Find(Id);
            db.Orders.Remove(dbEntity);
            db.SaveChanges();
        }
    }
}
