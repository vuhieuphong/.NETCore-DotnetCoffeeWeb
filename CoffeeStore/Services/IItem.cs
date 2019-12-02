using CoffeeStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Services
{
    public interface IItem
    {
        IEnumerable<Item> GetItems { get; }
        Item GetItem(int? Id);
        void Add(Item _Item);
        void Remove(int? Id);
    }
}
