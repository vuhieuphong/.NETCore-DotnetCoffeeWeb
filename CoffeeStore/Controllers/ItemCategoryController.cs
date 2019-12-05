using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeStore.Models;
using CoffeeStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStore.Controllers
{
    [Authorize]
    public class ItemCategoryController : Controller
    {
        private readonly IItemCategory _ItemCategory;
        public ItemCategoryController(IItemCategory _IItemCategory)
        {
            _ItemCategory = _IItemCategory;
        }
        public IActionResult Index(string sortOrder,string searchString)
        {
            ViewData["NameSortParm"] = sortOrder != "name_sort" ? "name_sort" : "";
            ViewData["currentFilter"] = searchString;
            var itemCategories = _ItemCategory.GetItemCategories;

            if (!string.IsNullOrEmpty(searchString))
            {
                itemCategories = itemCategories.Where(s => s.itemcategoryName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_sort":
                    itemCategories = itemCategories.OrderBy(s => s.itemcategoryName);
                    break;
                default:
                    itemCategories = itemCategories.OrderBy(s => s.itemcategoryID);
                    break;
            }
            return View(itemCategories.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ItemCategory model = new ItemCategory();
            model.itemcategoryID = 0;
            return View(model);
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

        public IActionResult Edit(int? Id)
        {
            var model = _ItemCategory.GetItemCategory(Id);
            return View("Create",model);
        }

        public IActionResult Details(int? Id)
        {
            var model = _ItemCategory.GetItemCategory(Id);
            return View(model);
        }
    }
}