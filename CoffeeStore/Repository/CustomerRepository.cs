using CoffeeStore.Models;
using CoffeeStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Repository
{
    public class CustomerRepository:ICustomer
    {
        private readonly DB_Context db;
        public CustomerRepository(DB_Context _db)
        {
            db = _db;
        }
        public IEnumerable<Customer> GetCustomers => db.Customers;
        public Customer GetCustomer(int? Id)
        {
            Customer dbEntity = db.Customers.Find(Id);
            return dbEntity;
        }
        public void Add(Customer _Customer)
        {
            db.Customers.Add(_Customer);
            db.SaveChanges();
        }
        public void Remove(int? Id)
        {
            Customer dbEntity = db.Customers.Find(Id);
            db.Customers.Remove(dbEntity);
            db.SaveChanges();
        }
    }
}
