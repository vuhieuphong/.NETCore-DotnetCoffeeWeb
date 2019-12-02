using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeStore.Models;
using CoffeeStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrder _Order;
        private readonly ICustomer _Customer;
        public OrderController(IOrder _IOrder,ICustomer _ICustomer)
        {
            _Order = _IOrder;
            _Customer = _ICustomer;
        }
        public IActionResult Index(string searchString)
        {
            var orders = _Order.GetOrders;
            if (!string.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.Customers.customerName.ToUpper().Contains(searchString.ToUpper()));
            }    
            return View(orders.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Customers = _Customer.GetCustomers;
            return View();
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
    }
}