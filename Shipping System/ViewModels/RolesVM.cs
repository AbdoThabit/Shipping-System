using System.ComponentModel.DataAnnotations;

namespace Shipping_System.ViewModels
{
    public class RolesVM
    {
        public string? Id { get; set; }
        [Required(ErrorMessage ="اسم الصلاحية مطلوب")]
        public string Name { get; set; }
    }
}
