using Microsoft.AspNetCore.Identity;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;
namespace Shipping_System.BL.Repositories.TraderRepository
{
    public interface ITraderRepo
    {
        Task<List<TraderVM>> Get();
        Task<IdentityResult> Add(TraderVM Trader);
        Task<TraderVM> GetById(string id);
        Task<IdentityResult> Edit(ApplicationUser Trader);
        Task<IdentityResult> Delete(ApplicationUser Trader);
        Task<IdentityResult> AddRole();
    }
}
