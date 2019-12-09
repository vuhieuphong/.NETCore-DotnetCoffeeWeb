using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        [Required(ErrorMessage = "Name is Required")]
        [DisplayName("Name")]
        public string name { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string email { get; set; }
        [DisplayName("Phone")]
        [Required(ErrorMessage = "Phone Number is Required")]
        public int phone { get; set; }
        [DisplayName("Order Time")]
        [Required(ErrorMessage ="Order Time is Required")]
        public DateTime orderTime { get; set; }
        [DisplayName("Order Details")]
        [BindNever]
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
