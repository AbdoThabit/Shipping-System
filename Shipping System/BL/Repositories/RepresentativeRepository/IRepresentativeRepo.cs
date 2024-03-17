using Microsoft.AspNetCore.Identity;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.RepresentativeRepository
{
    public interface IRepresentativeRepo
    {
        Task<List<RepresentativeVM>> Get();
        Task<IdentityResult> Add(RepresentativeVM Representative);
        Task<RepresentativeVM> GetById(string id);
        Task<IdentityResult> Edit(ApplicationUser Representative);
        Task<IdentityResult> Delete(ApplicationUser Representative);
        Task<IdentityResult> AddRole();
    }
}
