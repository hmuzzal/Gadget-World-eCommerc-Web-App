using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GadgetWorld.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }


        //[Required(ErrorMessage = "Something went wrong. Please Try Again Later")]
        public String Type { get; set; }

        public string Name { get; set; }


        [Required(ErrorMessage = "Please Enter Your Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        [DisplayName("Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public string DateOfBirth { get; set; }


        [Required(ErrorMessage = "Please Enter Password")]

        [StringLength(50, MinimumLength = 7)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [NotMapped]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Please Re-Enter Your Password or Password Doesn't Match")]
        public string RepeatPassword { get; set; }

        [NotMapped]
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}