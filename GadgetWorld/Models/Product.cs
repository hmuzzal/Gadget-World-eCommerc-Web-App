using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GadgetWorld.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [DisplayName("Product Category")]
        public string CategoryName { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string Color { get; set; }     
    
        public int Quantity { get; set; }

        //[DisplayName("Image")]
        //public string ImageLink { get; set; }


        public byte[] ImageData { get; set; }

        [NotMapped]
        public Category Category { get; set; }             


    }
}