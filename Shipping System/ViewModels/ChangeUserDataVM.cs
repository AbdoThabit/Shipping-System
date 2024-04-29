using System.ComponentModel.DataAnnotations;

namespace Shipping_System.ViewModels
{
    public class ChangeUserDataVM
    {
        [Required(ErrorMessage = "الاسم الكامل مطلوب")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "العنوان مطلوب")]
        public string Address { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "بريد الكتروني غير صالح")]
        [Required(ErrorMessage = "البريد الاكتروني مطلوب")]
        public string Email { get; set; }


        [RegularExpression(@"^01[0-2]{1}[0-9]{8}$", ErrorMessage = "رقم الهاتف غير صالح")]
        [Required(ErrorMessage = "رقم الهاتف مطلوب")]

        public string PhoneNumber { get; set; }
    }
}
