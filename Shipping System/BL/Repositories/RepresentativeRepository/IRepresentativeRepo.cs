using Microsoft.AspNetCore.Identity;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.RepresentativeRepository
{
    public interface IRepresentativeRepo
    {
        Task<List<RepresentativeRegistrationVM>> Get();
        Task<IdentityResult> Add(RepresentativeRegistrationVM Representative);
        Task<RepresentativeVM> GetById(string id);
        Task<IdentityResult> Edit(RepresentativeVM Representative);
        Task<IdentityResult> Delete(string id);
        Task<IdentityResult> AddRole();
        Task<RepresentativeRegistrationVM> IncludeLists();
        Task<List<RepresetivecheckListVm>> getAllRepresentivesOfBranch(int branchId);
    }
}
