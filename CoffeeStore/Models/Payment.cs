using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Models
{
    public enum PaymentType
    {
        Cash,Debit,Credit
    }

    public class Payment
    {
        [Key]
        public int paymentID { get; set; }
        [DisplayName("Customer Name")]
        public int customerID { get; set; }
        [DisplayName("Payment Amount")]
        [Required(ErrorMessage = "Payment Amount is Required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive amount")]
        public double paymentAmount { get; set; }
        [DisplayName("Payment Type")]
        public PaymentType paymentType { get; set; }
        public Customer Customers { get; set; }
    }
}
