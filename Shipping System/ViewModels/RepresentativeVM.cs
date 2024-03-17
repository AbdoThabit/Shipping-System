using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Shipping_System.ViewModels
{
    public class RepresentativeVM
    {
        [AllowNull]
        public string? Id { get; set; }

        [Required(ErrorMessage = "الاسم الكامل مطلوب")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "العنوان مطلوب")]
        public string Address { get; set; }
        [Required(ErrorMessage = "*")]

        public int? type_of_discount { get; set; }
        [Required(ErrorMessage = "*")]

        public int? company_percantage { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "بريد الكتروني غير صالح")]
        [Required(ErrorMessage = "البريد الاكتروني مطلوب")]

        public string Email { get; set; }

        [RegularExpression(@"^01[0-2]{1}[0-9]{8}$", ErrorMessage = "رقم الهاتف غير صالح")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "كلمتا المرور غير متطابقتان")]
        [Required(ErrorMessage = "تاكيد كلمة المرور مطلوبة")]

        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = " الفرع مطلوب")]
        public int Branch_Id { get; set; }

        [Required(ErrorMessage = " المدينة مطلوبة")]
        public int City_Id { get; set; }

        [Required(ErrorMessage = "المحافظة مطلوبة")]
        public int Governate_Id { get; set; }
    }
}
