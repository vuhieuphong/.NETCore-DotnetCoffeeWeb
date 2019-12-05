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
        public IActionResult Index(string sortOrder,string searchString)
        {
            ViewData["NameSortParm"] = sortOrder != "name_sort" ? "name_sort" : "";
            ViewData["DateSortParm"] = sortOrder != "date_sort" ? "date_sort" : "";
            ViewData["PriceSortParm"] = sortOrder != "price_sort" ? "price_sort" : "";
            ViewData["QuantitySortParm"] = sortOrder != "quantity_sort" ? "quantity_sort" : "";
            ViewData["currentFilter"] = searchString;
            var orderDetails = _OrderDetail.GetOrderDetails;

            if(!string.IsNullOrEmpty(searchString))
            {
                orderDetails=orderDetails.Where(ss => ss.Items.itemName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_sort":
                    orderDetails = orderDetails.OrderBy(s => s.Items.itemName);
                    break;
                case "date_sort":
                    orderDetails = orderDetails.OrderBy(s => s.Orders.orderTime);
                    break;
                case "price_sort":
                    orderDetails = orderDetails.OrderBy(s => s.price);
                    break;
                case "quantity_sort":
                    orderDetails = orderDetails.OrderBy(s => s.quantity);
                    break;
                default:
                    orderDetails = orderDetails.OrderBy(s => s.orderDetailID);
                    break;
            }

            return View(orderDetails.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            OrderDetail model = new OrderDetail();
            model.orderDetailID = 0;
            ViewBag.Orders = _Order.GetOrders;
            ViewBag.Items = _Item.GetItems;
            return View(model);
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

        public IActionResult Edit(int? Id)
        {
            var model = _OrderDetail.GetOrderDetail(Id);
            ViewBag.Orders = _Order.GetOrders;
            ViewBag.Items = _Item.GetItems;
            return View("Create", model);
        }

        public IActionResult Details(int? Id)
        {
            var model = _OrderDetail.GetOrderDetail(Id);
            return View(model);
        }
    }
}