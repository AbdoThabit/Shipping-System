using Shipping_System.DAL.Entites;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Shipping_System.ViewModels
{
    public class RepresentativeVM
    {
        [AllowNull]
        public string? Id { get; set; }

        [Required(ErrorMessage = "الاسم الكامل مطلوب")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "العنوان مطلوب")]
        public string Address { get; set; }

        [Required(ErrorMessage = "نوع الخصم مطلوب")]
        public int? type_of_discount { get; set; }

        [Required(ErrorMessage = " قيمة الشركة من الخصم مطلوبة")]
        public int? company { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "بريد الكتروني غير صالح")]
        [Required(ErrorMessage = "البريد الاكتروني مطلوب")]

        public string Email { get; set; }

        [RegularExpression(@"^01[0-2]{1}[0-9]{8}$", ErrorMessage = "رقم الهاتف غير صالح")]
        [Required(ErrorMessage = "رقم الهاتف مطلوب")]

        public string PhoneNumber { get; set; }

      

        [Required(ErrorMessage = " الفرع مطلوب")]
        public int Branch_Id { get; set; }

        [Required(ErrorMessage = " المدينة مطلوبة")]
        public int City_Id { get; set; }

        [Required(ErrorMessage = "المحافظة مطلوبة")]
        public int Governate_Id { get; set; }
        public string? GovernateName { get; set; }
        public string? CityName { get; set; }
        public string? BranchName { get; set; }
        public List<Governate>? Governates { get; set; }
        public List<City>? Cities { get; set; }
        public List<Branch>? Branches { get; set; }

    }
}
