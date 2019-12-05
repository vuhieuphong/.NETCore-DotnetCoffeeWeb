using CoffeeStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Components
{
    public class NavigationMenuViewComponent:ViewComponent
    {
        private IItem _Item;
        public NavigationMenuViewComponent(IItem _IItem)
        {
            _Item = _IItem;
        }

        public IViewComponentResult Invoke()
        {
            return View(_Item.GetItems
                .Select(x => x.ItemCategories.itemcategoryName)
                .Distinct()
                .OrderBy(x => x));
        } 
    }
}
