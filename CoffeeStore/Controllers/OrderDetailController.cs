using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeStore.Models;
using CoffeeStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStore.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly IOrderDetail _OrderDetail;
        private readonly IOrder _Order;
        private readonly IItem _Item;
        public OrderDetailController(IOrderDetail _IOrderDetail,IOrder _IIOrder,IItem _IItem)
        {
            _OrderDetail = _IOrderDetail;
            _Order = _IIOrder;
            _Item = _IItem;

        }
        public IActionResult Index(string searchString1,string searchString2)
        {
            var orderDetails = _OrderDetail.GetOrderDetails;
            if (!string.IsNullOrEmpty(searchString1))
            {
                orderDetails = orderDetails
                    .Where(s => s.Orders.Customers.customerName.ToUpper().Contains(searchString1.ToUpper()));                    
            }
            if(!string.IsNullOrEmpty(searchString2))
            {
                orderDetails=orderDetails.Where(ss => ss.Items.itemName.ToUpper().Contains(searchString2.ToUpper()));
            }
            return View(orderDetails.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Orders = _Order.GetOrders;
            ViewBag.Items = _Item.GetItems;
            return View();
        }
        [HttpPost]
        public IActionResult Create(OrderDetail model)
        {
            if(ModelState.IsValid)
            {
                _OrderDetail.Add(model);
                return RedirectToAction("Index");
            }
            ViewBag.Orders = _Order.GetOrders;
            ViewBag.Items = _Item.GetItems;
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
                OrderDetail model = _OrderDetail.GetOrderDetail(Id);
                return View(model);
            }
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirm(int? Id)
        {
            _OrderDetail.Remove(Id);
            return RedirectToAction("Index");
        }
    }
}