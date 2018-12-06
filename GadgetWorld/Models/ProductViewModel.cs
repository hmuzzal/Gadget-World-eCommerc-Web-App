using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GadgetWorld.Models
{
    public class ProductViewModel
    {
        [Key]
        public int ProductId { get; set; }

        [DisplayName("Product Category")]

        [Display(Name = "Select Category")]
        [Required(ErrorMessage = "Please Select Category")]
        public string Category { get; set; }

        [DisplayName("Select Product")]
        [Required(ErrorMessage = "Please Select Product")]
        public string ProductName { get; set; }

        [DisplayName("Select Color")]
        [Required(ErrorMessage = "Please Select Color")]
        public string ColorType { get; set; }
    }
}