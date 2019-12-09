using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeStore.Infrastructures;
using CoffeeStore.Models;
using CoffeeStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IItem _Item;
        private Cart cart;

        public CartController(IItem _IItem, Cart cartService)
        {
            _Item = _IItem;
            cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            { Cart = cart,
              ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int itemID, string returnUrl,int quantity)
        {
            Item item = _Item.GetItems.FirstOrDefault(p => p.itemID == itemID);
            if (item != null)
            {
                cart.AddItem(item, quantity);               
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int itemID, string returnUrl)
        {
            Item item = _Item.GetItems.FirstOrDefault(p => p.itemID == itemID);
            if (item != null)
            {
                cart.RemoveLine(item);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}