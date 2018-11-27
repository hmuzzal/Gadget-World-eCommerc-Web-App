using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GadgetWorld.Models
{
    public class UserType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }

        public static implicit operator UserType(string v)
        {
            throw new NotImplementedException();
        }
    }
}