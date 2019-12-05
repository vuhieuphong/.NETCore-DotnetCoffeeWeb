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
    public class CustomerController : Controller
    {
        private readonly ICustomer _Customer;
        public CustomerController(ICustomer _ICustomer)
        {
            _Customer = _ICustomer;
        }

        public IActionResult Index(string sortOrder,string searchString)
        {
            ViewData["NameSortParm"] = sortOrder != "name_sort" ? "name_sort" : "";
            ViewData["currentFilter"] = searchString;

            var customers = _Customer.GetCustomers;
            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.customerName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch(sortOrder)
            {
                case "name_sort":
                    customers = customers.OrderBy(s => s.customerName);
                    break;
                default:
                    customers = customers.OrderBy(s => s.customerID);
                    break;
            }

            return View(customers.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            Customer model = new Customer();
            model.customerID = 0;
            return View(model);
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

        public IActionResult Edit(int? Id)
        {
            var model = _Customer.GetCustomer(Id);
            return View("Create", model);
        }    

        public IActionResult Details(int? Id)
        {
            var model = _Customer.GetCustomer(Id);
            return View(model);
        }
    }
}