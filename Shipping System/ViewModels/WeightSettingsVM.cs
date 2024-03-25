using System.ComponentModel.DataAnnotations;

namespace Shipping_System.ViewModels
{
    public class WeightSettingsVM
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "  وزن الشحن الأســاسي مطلوب")]
        public int Default_Weight { get; set; }
        [Required(ErrorMessage = "  وزن الشحن الأضــافي مطلوب")]
        public int Extra_Weight { get; set; }
    }
}
