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
                                                                          .Include(f=>f.Items);

        public void Add(OrderDetail _OrderDetail)
        {
            if(_OrderDetail.orderDetailID==0)
            {
                db.OrderDetails.Add(_OrderDetail);
                db.SaveChanges();
            }
            else
            {
                db.OrderDetails.Update(_OrderDetail);
                db.SaveChanges();
            }
          
        }

        public OrderDetail GetOrderDetail(int? Id)
        {
            OrderDetail dbEntity = db.OrderDetails.Include(e => e.Orders)
                                                  .Include(g => g.Items)
                                                  .SingleOrDefault(f=>f.orderDetailID==Id);
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
