using CoffeeStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Services
{
    public interface IItemCategory
    {
        IEnumerable<ItemCategory> GetItemCategories { get; }
        ItemCategory GetItemCategory(int? Id);
        void Add(ItemCategory _ItemCategory);
        void Remove(int? Id);
    }
}
