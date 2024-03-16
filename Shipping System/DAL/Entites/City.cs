using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping_System.DAL.Entites
{
    public class City
    {
        public int Id { get; set;}

       [Column(TypeName = "nvarchar(20)")]
        [Required]
        public string Name { get; set;}
       public int  Shipping_Cost { get; set;}

        [ForeignKey("Governate")]
        public int Governate_Id {  get; set;}
        public virtual Governate Governate { get; set;}
        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<Order> Orders { get; set; }


    }
}
