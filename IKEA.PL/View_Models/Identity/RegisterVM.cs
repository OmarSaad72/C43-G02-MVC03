using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.View_Models.Identity
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "First Name Is Required")]
        [Display(Name = "First Name")]
        public string FName { get; set; }
        [Required(ErrorMessage = "Last Name Is Required")]
        [Display(Name = "Last Name")]
        public string LName { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password Doesn't Match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
