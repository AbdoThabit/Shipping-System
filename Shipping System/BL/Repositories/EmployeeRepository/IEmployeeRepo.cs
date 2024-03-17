using Microsoft.AspNetCore.Identity;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.EmployeeRepository
{
    public interface IEmployeeRepo
    {
     Task<List<EmployeeVM>> Get();
     Task<IdentityResult> Add(EmployeeVM Employee);
     Task<EmployeeUpdateVM> GetById(string id);
     Task<IdentityResult> Edit(EmployeeUpdateVM Employee);
     Task<IdentityResult> Delete(string id);
     Task<IdentityResult> AddRole();

    }
}
