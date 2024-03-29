﻿using CoffeeStore.Infrastructures;
using CoffeeStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Repository
{
    public class SessionCart : Cart    
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session; return cart;
        }
        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddItem(Item item, int quantity)
        {
            base.AddItem(item, quantity);
            Session.SetJson("Cart", this);
        }
        public override void RemoveLine(Item item)
        {
            base.RemoveLine(item);
            Session.SetJson("Cart", this);
        }
        public override void Clear()
        {
            base.Clear(); Session.Remove("Cart");
        }
    }
}
