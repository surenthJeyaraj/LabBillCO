using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Web.Models.Account
{
    public class ConfirmPasswordForTheAccountModel
    {//(?=.{8,16})(?=(.*\d){1,})(?=(.*\W){1,})
        
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d{1,})(?=.*[A-Z]{1,})(?=.*[a-z]{1,})(?=.*[$-/:-?{-~!^@#_`\[\]]{1,})(?=.*\w).{8,16}$", ErrorMessage = "The password should be 8 to 16 characters long with atleast one number, one upper & lowercase and one symbol")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Password and Re-type Password should be same")]
        [Display(Name = "Re-type Password")]
        public string ConfirmPassword { get; set; }

        public string ConfirmationToken { get; set; }
    }
}