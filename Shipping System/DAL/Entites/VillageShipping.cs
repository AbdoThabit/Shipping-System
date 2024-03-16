using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping_System.DAL.Entites
{
    public class VillageShipping
    {
        public int Id { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
