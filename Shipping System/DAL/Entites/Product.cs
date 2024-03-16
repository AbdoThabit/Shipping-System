using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping_System.DAL.Entites
{
    public class Product
    {
        public int Id { get; set;}

        [Column(TypeName = "nvarchar(20)")]
        [Required]
        public string Name { get; set;}

        public int Qunatity { get; set; }
        public int Weight { get; set;}

        [Column(TypeName = "money")]
        public decimal Price { get; set;}
        [ForeignKey("Order")]
        public int Order_Id { get; set; }
         public virtual Order Order { get; set; }

    }
}
