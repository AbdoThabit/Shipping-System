using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping_System.DAL.Entites
{
    public class Governate
    {
        public int Id { get; set;}

        [Column(TypeName = "nvarchar(20)")]
        [Required]

        public string Name { get; set;}
        public virtual ICollection<ApplicationUser> Users { get; set;}
        public virtual ICollection<City> Cities { get; set; }

        public virtual ICollection<Order> Orders { get; set; }






    }
}
