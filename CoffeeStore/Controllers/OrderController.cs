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
    public class OrderController : Controller
    {
        private readonly IOrder _Order;
        private readonly ICustomer _Customer;
        public OrderController(IOrder _IOrder,ICustomer _ICustomer)
        {
            _Order = _IOrder;
            _Customer = _ICustomer;
        }
        public IActionResult Index(string sortOrder,string searchString)
        {
            ViewData["NameSortParm"] = sortOrder != "name_sort" ? "name_sort" : "";
            ViewData["DateSortParm"] = sortOrder != "date_sort" ? "date_sort" : "";
            ViewData["CurrentFilter"] = searchString;

            var orders = _Order.GetOrders;
            if (!string.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.Customers.customerName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_sort":
                    orders = orders.OrderBy(s => s.Customers.customerName);
                    break;
                case "date_sort":
                    orders = orders.OrderBy(s => s.orderTime);
                    break;
                default:
                    orders = orders.OrderBy(s => s.orderID);
                    break;
            }

            return View(orders.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            Order model = new Order();
            model.orderID=0;
            ViewBag.Customers = _Customer.GetCustomers;
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Order model)
        {
            if(ModelState.IsValid)
            {
                _Order.Add(model);
                return RedirectToAction("Index");
            }
            ViewBag.Customers = _Customer.GetCustomers;
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
                Order model = _Order.GetOrder(Id);
                return View(model);
            }
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirm(int? Id)
        {
            _Order.Remove(Id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? Id)
        {
            var model = _Order.GetOrder(Id);
            ViewBag.Customers = _Customer.GetCustomers;
            return View("Create", model);
        }

        public IActionResult Details(int? Id)
        {
            var model = _Order.GetOrder(Id);
            return View(model);
        }
    }
}