using Shipping_System.DAL.Entites;
using System.ComponentModel.DataAnnotations;

namespace Shipping_System.ViewModels
{
    public class OrderStatusVM
    {

        public int OrderId { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب*")]
        public int StatusId { get; set; }
        public List<Order_Status> Status { get; set; }
    }
}
