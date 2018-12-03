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
        public int CategoryId { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string Color { get; set; }     
    
        public int Quantity { get; set; }

        //[DisplayName("Image")]
        //public string ImageLink { get; set; }


        [DisplayName("Upload Image")]
        public string ImagePath { get; set; }

        public string Title { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

    }
}