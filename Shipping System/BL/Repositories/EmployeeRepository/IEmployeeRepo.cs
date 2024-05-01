using Microsoft.AspNetCore.Identity;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.EmployeeRepository
{
    public interface IEmployeeRepo
    {
     Task<List<EmployeeRegistrationVM>> Get();
     Task<IdentityResult> Add(EmployeeRegistrationVM Employee);
     Task<EmployeeVM> GetById(string id);
     Task<IdentityResult> Edit(EmployeeVM Employee);
     Task<IdentityResult> Delete(string id);
     Task<IdentityResult> AddRole();
     Task<EmployeeRegistrationVM> IncludeLists();
     Task<List<EmployeeRolesVM>> GetCheckedEmployees();
     Task<IdentityResult> editEmployeeRole(List<EmployeeRolesVM> employeeAdminVMs);

    }
}
