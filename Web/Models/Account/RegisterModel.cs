using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Web.Models.Account
{
    public class RegisterModel
    {


        public int? UserId { get; set; }

        [Required(ErrorMessage = "Username field is required.")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Firstname")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Lastname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [Display(Name = "Role")]
        public string SelectedRole { get; set; }

        public IList<string> Roles { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid Email address")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

       
    }



    public class ShowMessageModel
    {
        public string Message { get; set; }
        public string ActionLink { get; set; }
        public string ActionMessage { get; set; }
    }






    public class ForgotPasswordModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid Email address")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

    }

    public class ResetPasswordModel
    {
        public string UserName { get; set; }
        public string PasswordResetToken { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d{1,})(?=.*[A-Z]{1,})(?=.*[a-z]{1,})(?=.*[$-/:-?{-~!^@_`\[\]]{1,})(?=.*\w).{8,16}$", ErrorMessage = "The password should be 8 to 16 characters long with atleast one number, one upper & lowercase and one symbol")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Both the passwords should be same")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }

}