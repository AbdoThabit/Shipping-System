using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shipping_System.ViewModels
{
    public class ShippingSettingVM
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "نوع الشحن مطلوب")]
        public string Shipping_Type { get; set; }

        [Required(ErrorMessage = "سعر الشحن مطلوب")]
        public decimal Shipping_Price { get; set; }

        [Required(ErrorMessage = "وصف الشحن مطلوب")]
        public string Shipping_Description { get; set; }
    }
}
