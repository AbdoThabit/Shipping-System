using Microsoft.AspNetCore.Identity;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;
namespace Shipping_System.BL.Repositories.TraderRepository
{
    public interface ITraderRepo
    {
        Task<List<TraderRegistrationVM>> Get();
        Task<IdentityResult> Add(TraderRegistrationVM Trader);
        Task<TraderVM> GetById(string id);
        Task<IdentityResult> Edit(TraderVM Trader );
        Task<IdentityResult> Delete(string id);
        Task<IdentityResult> AddRole();
        Task<TraderRegistrationVM> IncludeLists();


    }
}
