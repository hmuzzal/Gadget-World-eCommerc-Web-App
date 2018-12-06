using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GadgetWorld.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [DisplayName("Product Category")]

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please Select Category")]
        public string Category { get; set; }

        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Please Select Product")]
        public string ProductName { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string Color { get; set; }     
    
        public int Quantity { get; set; }

        //[DisplayName("Image")]
        //public string ImageLink { get; set; }


        [DisplayName("Image")]
        [Required(ErrorMessage = "Please Select Product Image")]
        public string ImagePath { get; set; }


        [NotMapped]
        public string Title { get; set; }

        [NotMapped]
        [DisplayName("Upload Image")]
        [Required(ErrorMessage = "Please Select Product Image")]
        public HttpPostedFileBase ImageFile { get; set; }

    }
}