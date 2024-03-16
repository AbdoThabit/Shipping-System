using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping_System.DAL.Entites
{
    public class ShippingSetting
    {
        public int Id { get; set; }
        [Required]
        public string Shipping_Type { get; set;}

        [Column(TypeName = "money")]
        public decimal Shipping_Price { get; set;}

        [Column(TypeName = "nvarchar(75)")]
        [Required]

        public string Shipping_Description {get; set;}
        public virtual ICollection<Order> Orders { get; set; }


        //order


    }
}
