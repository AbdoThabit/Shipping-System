using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.BranchRepository
{
    public interface IBranchRepo
    {
        Task<List<BranchVM>> Get();
        Task<int> Add(BranchVM Branch);
        Task<BranchVM> GetById(int id);
        Task<int> Edit(BranchVM Branch);
        Task<int> Delete(int id);
        Task<BranchVM> IncludeLists();
        Task<List<BranchVM>> GetByCityId(int id);
    }
}
