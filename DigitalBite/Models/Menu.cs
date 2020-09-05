using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalBite.Models
{
    public class Menu
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Starting letter should be capitalised!")]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Starting letter should be capitalised!")]
        [Display(Name = "Type / Category")]
        public string Type { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price (RM)")]
        public decimal Price { get; set; }
    }
}
