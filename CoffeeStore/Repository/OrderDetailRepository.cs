using CoffeeStore.Models;
using CoffeeStore.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Repository
{
    public class OrderDetailRepository : IOrderDetail
    {
        private readonly DB_Context db;
        public OrderDetailRepository(DB_Context _db)
        {
            db = _db;
        }
        public IEnumerable<OrderDetail> GetOrderDetails => db.OrderDetails.Include(e=>e.Orders)
                                                                          .ThenInclude(g=>g.Customers)
                                                                          .Include(f=>f.Items);

        public void Add(OrderDetail _OrderDetail)
        {
            db.OrderDetails.Add(_OrderDetail);
            db.SaveChanges();
        }

        public OrderDetail GetOrderDetail(int? Id)
        {
            OrderDetail dbEntity = db.OrderDetails.Find(Id);
            return dbEntity;
        }

        public void Remove(int? Id)
        {
            OrderDetail dbEntity = db.OrderDetails.Find(Id);
            db.OrderDetails.Remove(dbEntity);
            db.SaveChanges();
        }
    }
}
