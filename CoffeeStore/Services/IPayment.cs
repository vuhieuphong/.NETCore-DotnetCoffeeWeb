using CoffeeStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Services
{
    public interface IPayment
    {
        IEnumerable<Payment> GetPayments { get; }
        Payment GetPayment(int? Id);
        void Add(Payment _Payment);
        void Remove(int? Id);
    }
}
