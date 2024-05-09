using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping_System.BL.Helper;
using Shipping_System.ViewModels;
using System.Linq;
using System.Security.Claims;


namespace Shipping_System.BL.Repositories.RolesRepository
{
    public class RolesRepo : IRolesRepo
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly PermissionManger _permissionManger;

        public RolesRepo(RoleManager<IdentityRole> roleManager, PermissionManger permissionManger)
        {
            _roleManager = roleManager;
            _permissionManger = permissionManger;
        }

        public Task<IdentityResult> CreateRole(RolesVM role)
        {
            var Role = new IdentityRole
            {
                Name = role.Name
            };
            var state = _roleManager.CreateAsync(Role);
            return state;
        }

        public async Task<IQueryable<RolesVM>> GetRoles()
        {
            var roles = await _roleManager.Roles.Select(a => new RolesVM
            {
                Id = a.Id,
                Name = a.Name

            }).ToListAsync();

            return roles.AsQueryable();
        }

        public Task<IdentityRole> GetRoleById(string id)
        {
            var role = _roleManager.FindByIdAsync(id);


            return role;


        }

        public Task<IdentityResult> UpdateRole(IdentityRole role)
        {
            var state = _roleManager.UpdateAsync(role);
            return state;

        }

        public Task<IdentityResult> DeleteRole(string id, IdentityRole role)
        {
            var state = _roleManager.DeleteAsync(role);
            return state;
        }

        public async Task<RolesPermissioncsVM> GetRolesPermissioncs(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            RolesPermissioncsVM rolesPermissioncsVM = null;
            if (role != null)
            {
                var roles_claims = _roleManager.GetClaimsAsync(role).Result.Select(c => c.Value);
                rolesPermissioncsVM = new RolesPermissioncsVM()
                {
                    RoleName = role.Name,
                    RoleId = role.Id,
                    employeesPermissions = _permissionManger.getEmployeesPermisssions(),
                    tradersPermissions = _permissionManger.getTradersPermisssions(),
                    representivesPermissions = _permissionManger.getRepresentivesPermisssions(),
                    governatesPermissions = _permissionManger.getGovernatesPermisssions(),
                    citiesPermissions = _permissionManger.getCitiesPermisssions(),
                    branchsPermissions = _permissionManger.getBranchsPermisssions(),
                    ordersPermissions = _permissionManger.getOrdersPermisssions(),
                    rolesPermissions = _permissionManger.getRolesPermisssions(),
                    shippingPermissions = _permissionManger.getShippingSettingsPermisssions(),
                    weightPermissions = _permissionManger.getWeightSettingsPermisssions(),
                    valigePermissions = _permissionManger.getVillageSettingsPermisssions(),
                };
                foreach (Permission permission in rolesPermissioncsVM.employeesPermissions)
                {
                    if (roles_claims.Any(c => c == permission.Name))
                        permission.isExtiedToTheRole = true;
                }
                foreach (Permission permission in rolesPermissioncsVM.tradersPermissions)
                {
                    if (roles_claims.Any(c => c == permission.Name))
                        permission.isExtiedToTheRole = true;
                }
                foreach (Permission permission in rolesPermissioncsVM.representivesPermissions)
                {
                    if (roles_claims.Any(c => c == permission.Name))
                        permission.isExtiedToTheRole = true;
                }
                foreach (Permission permission in rolesPermissioncsVM.rolesPermissions)
                {
                    if (roles_claims.Any(c => c == permission.Name))
                        permission.isExtiedToTheRole = true;
                }
                foreach (Permission permission in rolesPermissioncsVM.governatesPermissions)
                {
                    if (roles_claims.Any(c => c == permission.Name))
                        permission.isExtiedToTheRole = true;
                }
                foreach (Permission permission in rolesPermissioncsVM.citiesPermissions)
                {
                    if (roles_claims.Any(c => c == permission.Name))
                        permission.isExtiedToTheRole = true;
                }
                foreach (Permission permission in rolesPermissioncsVM.branchsPermissions)
                {
                    if (roles_claims.Any(c => c == permission.Name))
                        permission.isExtiedToTheRole = true;
                }
                foreach (Permission permission in rolesPermissioncsVM.ordersPermissions)
                {
                    if (roles_claims.Any(c => c == permission.Name))
                        permission.isExtiedToTheRole = true;
                }
                foreach (Permission permission in rolesPermissioncsVM.shippingPermissions)
                {
                    if (roles_claims.Any(c => c == permission.Name))
                        permission.isExtiedToTheRole = true;
                }
                foreach (Permission permission in rolesPermissioncsVM.weightPermissions)
                {
                    if (roles_claims.Any(c => c == permission.Name))
                        permission.isExtiedToTheRole = true;
                }
                foreach (Permission permission in rolesPermissioncsVM.valigePermissions)
                {
                    if (roles_claims.Any(c => c == permission.Name))
                        permission.isExtiedToTheRole = true;
                }
                return rolesPermissioncsVM;
            }
            return rolesPermissioncsVM;
        }
        public async Task<IdentityResult> updatePermissions(RolesPermissioncsVM rolesPermissioncsVM)
        {
            IdentityResult result = null;
            var role = await _roleManager.FindByIdAsync(rolesPermissioncsVM.RoleId);
            if (role != null)
            {
                var claims = await _roleManager.GetClaimsAsync(role);
                foreach (var claim in claims)
                {
                    await _roleManager.RemoveClaimAsync(role, claim);
                }
                List<Permission> selectedPermissions = rolesPermissioncsVM.employeesPermissions.Where(p => p.isExtiedToTheRole)
                                                       .Concat(rolesPermissioncsVM.tradersPermissions.Where(p => p.isExtiedToTheRole))
                                                       .Concat(rolesPermissioncsVM.representivesPermissions.Where(p => p.isExtiedToTheRole))
                                                       .Concat(rolesPermissioncsVM.governatesPermissions.Where(p => p.isExtiedToTheRole))
                                                       .Concat(rolesPermissioncsVM.citiesPermissions.Where(p => p.isExtiedToTheRole))
                                                       .Concat(rolesPermissioncsVM.branchsPermissions.Where(p => p.isExtiedToTheRole))
                                                       .Concat(rolesPermissioncsVM.rolesPermissions.Where(p => p.isExtiedToTheRole))
                                                       .Concat(rolesPermissioncsVM.shippingPermissions.Where(p => p.isExtiedToTheRole))
                                                       .Concat(rolesPermissioncsVM.weightPermissions.Where(p => p.isExtiedToTheRole))
                                                       .Concat(rolesPermissioncsVM.valigePermissions.Where(p => p.isExtiedToTheRole))
                                                       .Concat(rolesPermissioncsVM.ordersPermissions.Where(p => p.isExtiedToTheRole))
                                                       .ToList();

                foreach(Permission permission in selectedPermissions)
                {
                    result = await _roleManager.AddClaimAsync(role, new Claim(permission.Name, permission.Name));
                }
                return result;

            }
            return result;
        }
    }
}
