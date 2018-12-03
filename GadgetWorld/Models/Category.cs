using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GadgetWorld.Models
{
    public class Category
    {
        [Key]
        public int CatagoryId { get; set; }
        public string CatagoryType { get; set; }

        public List<Product> Products { get; set; }
    }
}