using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Models
{
    public class Login
    {
        [Required]
        [Display(Prompt ="Admin")]
        public string Name { get; set; }
        [Required]
        [Display(Prompt = "Phong99$")]
        [UIHint("password")] public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
