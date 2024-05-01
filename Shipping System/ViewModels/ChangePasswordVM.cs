using System.ComponentModel.DataAnnotations;

namespace Shipping_System.ViewModels
{
    public class ChangePasswordVM
    {
        [Required(ErrorMessage = " كلمة القديمة المرور مطلوب")]
        [DataType(DataType.Password)]
        public String OldPassword { get; set; }
        [Required(ErrorMessage = " كلمة المرور مطلوب")]
        [DataType(DataType.Password)]
        public String NewPassword { get; set; }
        [Required(ErrorMessage = "تأكيد كلمة المرور مطلوب")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "كلمة المرور غير متطابقه")]
        public String ConfirmNewPassword { get; set; }
    }
}
