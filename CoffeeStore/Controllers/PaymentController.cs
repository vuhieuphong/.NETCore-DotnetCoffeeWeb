using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeStore.Models;
using CoffeeStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStore.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPayment _Payment;
        private readonly ICustomer _Customer;
        public PaymentController(IPayment _IPayment,ICustomer _ICustomer)
        {
            _Payment = _IPayment;
            _Customer = _ICustomer;
        }

        public IActionResult Index(string searchString)
        {
            var payments = _Payment.GetPayments;
            if (!string.IsNullOrEmpty(searchString))
            {
                payments = payments.Where(s => s.Customers.customerName.ToUpper().Contains(searchString.ToUpper()));
            }
            return View(payments.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Customers = _Customer.GetCustomers;
            return View();
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
    }
}