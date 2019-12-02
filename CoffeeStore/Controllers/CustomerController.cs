using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeStore.Models;
using CoffeeStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomer _Customer;
        public CustomerController(ICustomer _ICustomer)
        {
            _Customer = _ICustomer;
        }

        public IActionResult Index(string searchString)
        {
            var customers = _Customer.GetCustomers;
            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.customerName.ToUpper().Contains(searchString.ToUpper()));
            }
            return View(customers.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer model)
        {
            if(ModelState.IsValid)
            {
                _Customer.Add(model);
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
                Customer model = _Customer.GetCustomer(Id);
                return View(model);
            }
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirm(int? Id)
        {
            _Customer.Remove(Id);
            return RedirectToAction("Index");
        }
    }
}