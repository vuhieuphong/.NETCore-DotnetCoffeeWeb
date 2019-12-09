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
        [DisplayName("Name")]
        public int orderID { get; set; }
        [DisplayName("Item Name")]
        public int itemID { get; set; }      
        [DisplayName("Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive quantity")]
        [Required(ErrorMessage ="Quantity is Required")]
        public int quantity { get; set; }
        public Order Orders { get; set; }
        public Item Items { get; set; }
    }
}
