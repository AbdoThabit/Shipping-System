using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Shipping_System.DAL.Entites
{
    public class Order
    {
        public int Id { get; set;}

        [Column(TypeName = "nvarchar(20)")]
        [Required]
        public string Client_Name { get; set;}

        [Column(TypeName = "nvarchar(80)")]
        [Required]

        public string Address { get; set;}


        [Column(TypeName = "nvarchar(30)")]
        [Required]
        public string Email {  get; set;}
        [Required]
        public string FristPhoneNumber { get; set;}

        public string? SecoundPhoneNumber { get; set;}
        public DateTime Order_Date { get; set;}
        public string? Village_Name {  get; set;}
        public bool Village_Flag { get; set;}
        public int Total_weight { get; set;}

        [Column(TypeName = "money")]
        public decimal Products_Total_Cost { get; set;}
        [Column(TypeName = "money")]
        public decimal Shipping_Total_Cost { get; set; }
        [Column(TypeName = "money")]
        public decimal Order_Total_Cost { get; set;}

        public int Payment_Type {  get; set;}
        [ForeignKey("Status")]
        public int Status_Id {  get; set;}
        public virtual Order_Status Status { get; set;}

        [Column(TypeName = "nvarchar(120)")]
        public string? Notes { get; set;}


        [ForeignKey("Governate")]
        public int Governate_Id { get; set; }
        public virtual Governate Governate { get; set; }


        [ForeignKey("City")]
        public int City_Id { get; set; }
        public virtual City City { get; set; }

        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public virtual Branch Branch { get; set;}

        [ForeignKey("ShippingSetting")]
        public int ShippingSetting_Id {  get; set; }

        public virtual ShippingSetting ShippingSetting { get; set; }

        [ForeignKey("WeightSetting")]

        public int? WeightSetting_Id { get; set;}
        public virtual WeightSetting WeightSetting { get; set;}
        [ForeignKey("VillageShipping")]

        public int? VillageSetting_Id { get; set;}
        public virtual VillageShipping VillageShipping { get; set; }
        [ForeignKey("Representitive")]
        public string Representitive_Id { get; set; }
        public virtual ApplicationUser Representitive { get; set; }
        [ForeignKey("Trader")]
        public string Trader_Id { get; set; }
        public virtual ApplicationUser Trader { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<Product> Products  { get; set; }








    }
}
