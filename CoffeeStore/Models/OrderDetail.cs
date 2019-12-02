using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Models
{
    public class OrderDetail
    {
        [Key]
        public int orderDetailID { get; set; }
        [DisplayName("Customer Name")]
        public int orderID { get; set; }
        [DisplayName("Item Name")]
        public int itemID { get; set; }
        [DisplayName("Price")]
        [Required(ErrorMessage ="Price is Required")]
        public double price { get; set; }
        [DisplayName("Quantity")]
        [Required(ErrorMessage ="Quantity is Required")]
        public int quantity { get; set; }
        public Order Orders { get; set; }
        public Item Items { get; set; }
    }
}
