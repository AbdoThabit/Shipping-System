using Microsoft.AspNetCore.Identity;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.GovernateRepository
{
    public interface IGovernateRepo
    {
      Task<List<GovernateVM>> Get();
       Task<int> Add(GovernateVM Governate);
        Task <GovernateVM> GetById(int id);
        Task<int> Edit(GovernateVM governate);
        Task<int> Delete(int id);
    }
}
