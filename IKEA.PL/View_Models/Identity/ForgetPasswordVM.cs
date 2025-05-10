using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.View_Models.Identity
{
    public class ForgetPasswordVM
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
    }
}
