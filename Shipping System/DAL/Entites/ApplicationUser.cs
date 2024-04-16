using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping_System.DAL.Entites
{
  
    public class ApplicationUser:IdentityUser
    {
        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string FullName { get; set;}

        [Column(TypeName = "nvarchar(80)")]
        [Required]
        public string Address { get; set; }
        public int? Trader_RejOrderPrec { get; set;}
        public int? type_of_discount { get; set;}
        public int? company_value { get; set;}
        public int? company_percantage {  get; set;}


        [ForeignKey("Governate")]
        public int Governate_Id { get; set;}
        public virtual Governate Governate { get; set;}


        [ForeignKey("City")]
        public int City_Id { get; set; }
        public virtual City City { get; set;}

        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public virtual Branch Branch { get; set; }
        [InverseProperty("Representitive")]
        public virtual ICollection<Order> RepresentiveOrders { get; set; }
        [InverseProperty("Trader")]
        public virtual ICollection<Order> TraderOrders { get; set; }










    }
}
