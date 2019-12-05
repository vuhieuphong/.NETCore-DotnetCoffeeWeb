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
    public class PaymentController : Controller
    {
        private readonly IPayment _Payment;
        private readonly ICustomer _Customer;
        public PaymentController(IPayment _IPayment,ICustomer _ICustomer)
        {
            _Payment = _IPayment;
            _Customer = _ICustomer;
        }

        public IActionResult Index(string sortOrder,string searchString)
        {
            ViewData["NameSortParm"] = sortOrder != "name_sort" ? "name_sort" : "";
            ViewData["AmountSortParm"] = sortOrder != "amount_sort" ? "amount_sort" : "";
            ViewData["currentFilter"] = searchString;

            var payments = _Payment.GetPayments;
            if (!string.IsNullOrEmpty(searchString))
            {
                payments = payments.Where(s => s.Customers.customerName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_sort":
                    payments = payments.OrderBy(s => s.Customers.customerName);
                    break;
                case "amount_sort":
                    payments = payments.OrderBy(s => s.paymentAmount);
                    break;
                default:
                    payments = payments.OrderBy(s => s.paymentID);
                    break;
            }

            return View(payments.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            Payment model = new Payment();
            model.paymentID = 0;
            ViewBag.Customers = _Customer.GetCustomers;
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(Payment model)
        {
            if (ModelState.IsValid)
            {
                _Payment.Add(model);
                return RedirectToAction("Index");
            }
            ViewBag.Customers = _Customer.GetCustomers;
            return View(model);
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            else
            {
                Payment model = _Payment.GetPayment(Id);
                return View(model);
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int? Id)
        {
            _Payment.Remove(Id);
            return RedirectToAction("Index");
        }
        
        public IActionResult Edit(int? Id)
        {
            var model = _Payment.GetPayment(Id);
            ViewBag.Customers = _Customer.GetCustomers;
            return View("Create",model);
        }
        
        public IActionResult Details(int? Id)
        {
            var model = _Payment.GetPayment(Id);
            return View(model);
        }
    }
}