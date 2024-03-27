using System.ComponentModel.DataAnnotations;

namespace Shipping_System.ViewModels
{
    public class WeightSettingsVM
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب*")]
        public int Default_Weight { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب*")]
        public int Extra_Weight { get; set; }
    }
}
