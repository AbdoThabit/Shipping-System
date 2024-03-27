using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.WeightSettingsRepository
{
    public interface IWeightSettingsRepo
    {
        Task<List<WeightSettingsVM>> Get();
        Task<int> Add(WeightSettingsVM weightvm);
        Task<WeightSettingsVM> GetWeightSettings();
        Task<int> Edit(WeightSettingsVM weightvm);
        Task<int> Delete(int id);
    }
}
