using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping_System.DAL.Entites
{
    public class Branch
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        [Required]
        public string Name { get; set;}
        public DateTime Creation_Date { get; set;}

        [ForeignKey("City")]
        public int City_Id {  get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set;}
        public virtual ICollection<Order> Orders { get; set; }


    }
}
