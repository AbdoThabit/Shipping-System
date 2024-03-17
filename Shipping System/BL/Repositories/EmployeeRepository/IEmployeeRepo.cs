using Microsoft.AspNetCore.Identity;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.EmployeeRepository
{
    public interface IEmployeeRepo
    {
     Task<List<EmployeeVM>> Get();
     Task<IdentityResult> Add(EmployeeVM Employee);
     Task<EmployeeVM> GetById(string id);
     Task<IdentityResult> Edit(ApplicationUser Employee);
     Task<IdentityResult> Delete(ApplicationUser Employee);
        Task<IdentityResult> AddRole();

    }
}
