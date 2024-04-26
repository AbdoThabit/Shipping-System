using System.ComponentModel.DataAnnotations;

namespace Shipping_System.ViewModels
{
    public class ForgetPasswordVM
    {
        [Required(ErrorMessage ="البريد الالكتروني مطلوب")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
