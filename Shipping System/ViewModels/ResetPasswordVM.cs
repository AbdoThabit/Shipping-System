using System.ComponentModel.DataAnnotations;

namespace Shipping_System.ViewModels
{
    public class ResetPasswordVM
    {

        [EmailAddress(ErrorMessage = "من فضلك قم بادخال بريد الكتروني صالح")]
        public string Email { get; set; }
        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [DataType(DataType.Password)]

        public String Password { get; set; }
        [Required(ErrorMessage = "تأكيد كلمة المرور مطلوب")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "كلمة المرور غير متطابقه")]
        public String ConfirmPassword { get; set; }
        public String Token { get; set; }
    }
}
