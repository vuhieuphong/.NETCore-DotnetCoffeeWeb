﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoffeeStore.Models;
using CoffeeStore.Services;

namespace CoffeeStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IItem _Item;
        public int pageSize = 3;

        public HomeController(IItem _IItem)
        {
            _Item = _IItem;
        }
        public IActionResult Index(string category, int? pageNumber)
        {
            ViewBag.SelectedCategory = category;
            var model = _Item.GetItems
                .Where(p => category == null || p.ItemCategories.itemcategoryName == category);
            return View(PaginatedList<Item>.Create(model, pageNumber ?? 1, pageSize));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
