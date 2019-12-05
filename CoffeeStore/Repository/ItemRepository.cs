using CoffeeStore.Models;
using CoffeeStore.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Repository
{
    public class ItemRepository:IItem
    {
        private readonly DB_Context db;
        public ItemRepository(DB_Context _db)
        {
            db = _db;
        }
        public IEnumerable<Item> GetItems => db.Items.Include(e=>e.ItemCategories);
        public Item GetItem(int? Id)
        {
            Item dbEntity = db.Items
                .Include(i=>i.ItemCategories)
                .Include(i=>i.OrderDetails)
                .ThenInclude(i=>i.Orders)
                .SingleOrDefault(i=>i.itemID==Id);
            return dbEntity;
        }
        public void Add(Item _Item)
        {
            if(_Item.itemID==0)
            {
                db.Items.Add(_Item);
                db.SaveChanges();
            }
            else
            {
                db.Items.Update(_Item);
                db.SaveChanges();
            }
        }
        public void Remove(int? Id)
        {
            Item dbEntity = db.Items.Find(Id);
            db.Items.Remove(dbEntity);
            db.SaveChanges();
        }
    }
}
