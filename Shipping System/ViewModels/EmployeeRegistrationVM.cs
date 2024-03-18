using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shipping_System.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public class EmployeeRegistrationVM:EmployeeVM
    {
        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "كلمتا المرور غير متطابقتان")]
        [Required(ErrorMessage = "تاكيد كلمة المرور مطلوبة")]

        public string? ConfirmPassword { get; set; }

    }

}
