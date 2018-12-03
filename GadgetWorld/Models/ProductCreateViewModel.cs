using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GadgetWorld.Models
{
    public class ProductCreateViewModel
    {
            [Key]
            public int ProductId { get; set; }

            [Display(Name = "Category")]
            public int? CategoryId { get; set; }
        
            [DisplayName("Product Name")]
            public string ProductName { get; set; }

            public string Description { get; set; }

            public int Price { get; set; }

            public string Color { get; set; }

            public int Quantity { get; set; }

            //[DisplayName("Image")]
            //public string ImageLink { get; set; 


            public byte[] ImageData { get; set; }

            public List<SelectListItem> CategoryList { get; set; }
        }
    }

