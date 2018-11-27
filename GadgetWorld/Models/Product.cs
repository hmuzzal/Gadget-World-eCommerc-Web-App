using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GadgetWorld.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [DisplayName("Product Catagory")]
        public int CataCatagoryId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string Color { get; set; }     
    
        public int Quantity { get; set; }

        [DisplayName("    ")]
        public string ImageLink { get; set; }

        public Catagory Catagory { get; set; }             


    }
}