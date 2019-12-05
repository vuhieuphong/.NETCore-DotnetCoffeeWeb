using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Models
{
    public class Item
    {
        [Key]
        public int itemID { get; set; }
        [DisplayName("Item Category Name")]
        public int itemcategoryID { get; set; }
        [DisplayName("Item Name")]
        [Required(ErrorMessage ="Item Name is Required")]
        public string itemName { get; set; }
        [DisplayName("Item Image")]
        public byte[] itemImage { get; set; }
        public ItemCategory ItemCategories { get; set; }
        [DisplayName("Order Details")]
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
