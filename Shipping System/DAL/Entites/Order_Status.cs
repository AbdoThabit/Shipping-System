using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shipping_System.DAL.Entites
{
    public class Order_Status
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [Required]
        public string Name { get; set;}
        public virtual ICollection<Order> Orders { get; set; }

    }
}
