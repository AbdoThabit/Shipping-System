using Shipping_System.DAL.Entites;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shipping_System.ViewModels
{
    public class CityVM
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        [Required]
        public string Name { get; set; }
        public int Shipping_Cost { get; set; }
        [Required(ErrorMessage = "المحافظة مطلوبة")]
        public int Governate_Id { get; set; }
        public List<Governate>? Governates { get; set; }
    }
}
