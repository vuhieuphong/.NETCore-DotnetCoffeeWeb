using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Models
{
    public class ItemCategory
    {
        [Key]
        public int itemcategoryID { get; set; }
        [DisplayName("Item Category Name")]
        [Required(ErrorMessage ="Item Category Name is Required")]
        public string itemcategoryName { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
