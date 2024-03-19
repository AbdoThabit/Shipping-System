using System.ComponentModel.DataAnnotations;

namespace Shipping_System.ViewModels
{
    public class RepresentativeRegistrationVM :RepresentativeVM
    {
        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "كلمتا المرور غير متطابقتان")]
        [Required(ErrorMessage = "تاكيد كلمة المرور مطلوبة")]

        public string? ConfirmPassword { get; set; }
    }
}
