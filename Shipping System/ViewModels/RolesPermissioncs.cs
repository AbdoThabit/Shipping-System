using Shipping_System.BL.Helper;

namespace Shipping_System.ViewModels
{
    public class RolesPermissioncsVM
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<Permission> employeesPermissions { get; set; }
        public List<Permission> tradersPermissions { get; set; }
        public List<Permission> representivesPermissions { get; set; }
        public List<Permission> governatesPermissions { get; set; }
        public List<Permission> citiesPermissions { get; set; }
        public List<Permission> branchsPermissions { get; set; }
        public List<Permission> ordersPermissions { get; set; }
        public List<Permission> rolesPermissions { get; set; }
        public List<Permission> shippingPermissions { get; set; }
        public List<Permission> weightPermissions { get; set; }
        public List<Permission> valigePermissions { get; set; }
    }
}
