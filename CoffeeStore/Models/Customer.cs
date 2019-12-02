using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Models
{
    public class Customer
    {
        [Key]
        public int customerID { get; set; }
        [DisplayName("Customer Name")]
        [Required(ErrorMessage ="Customer Name is Required")]
        public string customerName { get; set; }
        [DisplayName("Address")]
        [Required(ErrorMessage = "Address Name is Required")]
        public string address { get; set; }
        [DisplayName("Telephone Number")]
        [Required(ErrorMessage = "Telephone Number is Required")]
        public int telephone { get; set; }
        [DisplayName("Email")]
        [EmailAddress]
        public string email { get; set; }
    }
}
