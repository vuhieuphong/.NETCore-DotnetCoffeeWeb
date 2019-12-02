using CoffeeStore.Models;
using CoffeeStore.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Repository
{
    public class ItemCategoryRepository : IItemCategory
    {
        private readonly DB_Context db;
        public ItemCategoryRepository(DB_Context _db)
        {
            db = _db;
        }

        public IEnumerable<ItemCategory> GetItemCategories => db.ItemCategories;

        public void Add(ItemCategory _ItemCategory)
        {
            db.ItemCategories.Add(_ItemCategory);
            db.SaveChanges();
        }

        public ItemCategory GetItemCategory(int? Id)
        {
            ItemCategory dbEntity = db.ItemCategories.Find(Id);
            return dbEntity;
        }

        public void Remove(int? Id)
        {
            ItemCategory dbEntity = db.ItemCategories.Find(Id);
            db.ItemCategories.Remove(dbEntity);
            db.SaveChanges();
        }
    }
}
