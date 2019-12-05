using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Models
{
    public class Order
    {
        [Key]
        public int orderID { get; set; }
        [DisplayName("Customer Name")]
        public int customerID { get; set; }
        [DisplayName("Order Time")]
        [Required(ErrorMessage ="Order Time is Required")]
        public DateTime orderTime { get; set; }
        public Customer Customers { get; set; }
        [DisplayName("Order Details")]
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
