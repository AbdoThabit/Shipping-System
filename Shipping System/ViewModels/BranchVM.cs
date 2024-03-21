using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Shipping_System.DAL.Entites;

namespace Shipping_System.ViewModels
{
    public class BranchVM
    {
        public int? Id { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        [Required]
        public string Name { get; set; }
        public DateTime Creation_Date { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "المدينه مطلوبة")]

        public int City_Id { get; set; }
        public List<City>? Cities { get; set; }

    }
}
