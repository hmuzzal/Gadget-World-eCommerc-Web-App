using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GadgetWorld.Models
{
    public class Catagory
    {
        public int CatagoryId { get; set; }
        public string CatagoryType { get; set; }

        public List<Product> Products { get; set; }
    }
}