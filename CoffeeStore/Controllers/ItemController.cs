using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoffeeStore.Models;
using CoffeeStore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStore.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItem _Item;
        private readonly IItemCategory _ItemCategory;
        public ItemController(IItem _IItem,IItemCategory _IItemCategory)
        {
            _Item = _IItem;
            _ItemCategory = _IItemCategory;
        }
        public IActionResult Index(string searchString1,string searchString2)
        {
            var items = _Item.GetItems;
            if (!string.IsNullOrEmpty(searchString1))
            {
                items = items
                    .Where(s => s.ItemCategories.itemcategoryName.ToUpper().Contains(searchString1.ToUpper()));
            }
            if (!string.IsNullOrEmpty(searchString2))
            {
                items = items.Where(ss => ss.itemName.ToUpper().Contains(searchString2.ToUpper()));
            }
            return View(items.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            Item model = new Item();
            model.itemID = 0;
            ViewBag.ItemCategories = _ItemCategory.GetItemCategories;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("itemID,itemcategoryID,itemName")] Item model, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                using (var ms = new MemoryStream())
                {
                    imageFile.CopyTo(ms);
                    model.itemImage = ms.ToArray();
                }
                _Item.Add(model);
                return RedirectToAction("Index");
            }
            ViewBag.ItemCategories = _ItemCategory.GetItemCategories;
            return View(model);
        }

        public IActionResult Delete(int? Id)
        {
            if(Id==null)
            {
                return NotFound();
            }
            else
            {
                Item model = _Item.GetItem(Id);
                return View(model);
            }
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirm(int? Id)
        {
            _Item.Remove(Id);
            return RedirectToAction("Index");
        }
        
        public IActionResult Edit(int? Id)
        {
            var model = _Item.GetItem(Id);
            ViewBag.ItemCategories = _ItemCategory.GetItemCategories;
            return View("Create", model);
        }       
    }
}