using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.CityRepository
{
    public interface ICityRepo
    {
        Task<List<CityVM>> Get();
        Task<int> Add(CityVM City);
        Task<CityVM> GetById(int id);
        Task<int> Edit(CityVM city);
        Task<int> Delete(int id);
        Task<CityVM> IncludeLists();
        Task<List<CityVM>> GetByGovernateId(int id);
    }
}
