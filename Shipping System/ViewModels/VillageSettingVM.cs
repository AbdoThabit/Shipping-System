using Shipping_System.DAL.Entites;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping_System.ViewModels
{
    public class VillageSettingVM
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "  سعر الشــحن للقريـة مطلوب")]
        public decimal Price { get; set; }
    }
}
