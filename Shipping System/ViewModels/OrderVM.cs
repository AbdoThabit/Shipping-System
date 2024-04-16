using Shipping_System.DAL.Entites;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shipping_System.ViewModels
{
    public class OrderVM
    {
        public int? Id { get; set; }
    
        [Required]
        public string Client_Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FristPhoneNumber { get; set; }
        public string SecoundPhoneNumber { get; set; }
        public DateTime Order_Date { get; set; }
        public string Village_Name { get; set; }
        public bool Village_Flag { get; set; }
        public int? Total_weight { get; set;}
        public int OrderStatusId { get; set; }
        public decimal? Products_Total_Cost { get; set; }
        public decimal? Order_Total_Cost { get; set; }

        public int Payment_Type { get; set; }
        public int? Status_Id { get; set; }

        public string? Notes { get; set; }


        public int Governate_Id { get; set; }


        public int City_Id { get; set; }

        public int Branch_Id { get; set; }

        public int ShippingSetting_Id { get; set; }

        public int? WeightSetting_Id { get; set; }

        public int? VillageSetting_Id { get; set; }

        public string Representitive_Id { get; set; }
        public string Trader_Id { get; set; }
        public string? GovernateName { get; set; }
        public string? CityName { get; set; }
        public string? BranchName { get; set; }
        public string? RepresntiveName { get; set; }
        public string? TraderName { get; set; }


        public List<Governate>? Governates { get; set; }
        public List<City>? Cities { get; set; }
        public List<Branch>? Branches { get; set; }
        public List<ShippingSetting>?  shippingSettings { get; set; }
        public IList<ApplicationUser>? Representitve { get; set; }
        public IList<ApplicationUser>? Traders { get; set; }

        public IList<Order_Status>? Statuses { get; set; }
        [Required(ErrorMessage = "يجب ان يحتوي الطلب علي منتجات")]
        public List<Product> Products { get; set;}
        public List<int> product_Ids_To_Delete  { get; set;}
    }
}
