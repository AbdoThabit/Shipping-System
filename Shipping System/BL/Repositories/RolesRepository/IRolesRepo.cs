using Microsoft.AspNetCore.Identity;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.RolesRepository
{
    public interface IRolesRepo
    {
        Task<IdentityResult> CreateRole(RolesVM role);
        Task<IQueryable<RolesVM>> GetRoles();
        Task<IdentityRole> GetRoleById(string id);
        Task<IdentityResult> UpdateRole(IdentityRole role);
        Task<IdentityResult> DeleteRole(string id, IdentityRole role);
        Task<RolesPermissioncsVM> GetRolesPermissioncs(string id);
        Task<IdentityResult> updatePermissions(RolesPermissioncsVM rolesPermissioncsVM);
    }
}
