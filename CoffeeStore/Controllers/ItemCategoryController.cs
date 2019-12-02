using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeStore.Models;
using CoffeeStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStore.Controllers
{
    public class ItemCategoryController : Controller
    {
        private readonly IItemCategory _ItemCategory;
        public ItemCategoryController(IItemCategory _IItemCategory)
        {
            _ItemCategory = _IItemCategory;
        }
        public IActionResult Index(string searchString)
        {
            var itemCategories = _ItemCategory.GetItemCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                itemCategories = itemCategories.Where(s => s.itemcategoryName.ToUpper().Contains(searchString.ToUpper()));
            }
            return View(itemCategories.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ItemCategory model)
        {
            if(ModelState.IsValid)
            {
                _ItemCategory.Add(model);
                return RedirectToAction("Index");
            }
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
                ItemCategory model = _ItemCategory.GetItemCategory(Id);
                return View(model);
            }
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirm(int? Id)
        {
            _ItemCategory.Remove(Id);
            return RedirectToAction("Index");
        }
    }
}