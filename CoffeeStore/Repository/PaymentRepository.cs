using CoffeeStore.Models;
using CoffeeStore.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Repository
{
    public class PaymentRepository : IPayment
    {
        private readonly DB_Context db;
        public PaymentRepository(DB_Context _db)
        {
            db = _db;
        }
        public IEnumerable<Payment> GetPayments => db.Payments.Include(e=>e.Customers);

        public void Add(Payment _Payment)
        {
            db.Payments.Add(_Payment);
            db.SaveChanges();
        }

        public Payment GetPayment(int? Id)
        {
            Payment dbEntity=db.Payments.Find(Id);
            return dbEntity;
        }

        public void Remove(int? Id)
        {
            Payment dbEntity = db.Payments.Find(Id);
            db.Payments.Remove(dbEntity);
            db.SaveChanges();
        }
    }
}
