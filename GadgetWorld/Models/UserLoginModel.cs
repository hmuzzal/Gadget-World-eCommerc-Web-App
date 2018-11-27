 using System;
using System.Collections.Generic;
 using System.ComponentModel;
 using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GadgetWorld.Models
{
    public class UserLoginModel
    {
        [Key]
        [Required(ErrorMessage = "Use Email Id to Login")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="Remember Me")]
        public bool RememberMe { get; set; }
    }
}