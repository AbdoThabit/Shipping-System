using Shipping_System.DAL.Entites;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping_System.ViewModels
{
    public class VillageSettingVM
    {

        [Required(ErrorMessage = "  سعر الشــحن للقريـة مطلوب")]
        public decimal Price { get; set; }
    }
}
