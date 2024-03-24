using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.ShippingSettingRepository
{
    public interface IShippingSettingRepo
    {
        Task<List<ShippingSettingVM>> Get();
        Task<int> Add(ShippingSettingVM shippingSetting);
        Task<ShippingSettingVM> GetById(int id);
        Task<int> Edit(ShippingSettingVM shippingSetting);
        Task<int> Delete(int id);
    }
}
