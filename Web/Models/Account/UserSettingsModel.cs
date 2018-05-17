using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.SqlServer.Server;

namespace Web.Models.Account
{
    public class UserSettingsModel
    {
        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }


        //[Display(Name = "Old password")]
        //[Required]
        //public string OldPassword { get; set; }

        //[Display(Name = "New password")]
        //[RegularExpression(@"^(?=.*\d{1,})(?=.*[A-Z]{1,})(?=.*[a-z]{1,})(?=.*[$-/:-?{-~!^@#_`\[\]]{1,})(?=.*\w).{8,16}$", ErrorMessage = "The password should be 8 to 16 in length with atleast one digit,one upper and lowercase alphabet and one symbol.")]
        //[Required]
        //public string NewPassword { get; set; }

        //[Display(Name = "Confirm password")]
        //public string ConfirmNewPassword { get; set; }
    }
}