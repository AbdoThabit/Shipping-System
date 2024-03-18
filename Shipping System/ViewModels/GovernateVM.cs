using System.ComponentModel.DataAnnotations;

namespace Shipping_System.ViewModels
{
    public class GovernateVM
    {
        public int? Id { get; set;}

        [Required(ErrorMessage = "اسم المحافظة مطلوب")]
        public string Name { get; set; }

    }
}
