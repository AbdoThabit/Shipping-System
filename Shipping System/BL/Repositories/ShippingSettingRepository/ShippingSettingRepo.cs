using Microsoft.EntityFrameworkCore;
using Shipping_System.DAL.Database;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.ShippingSettingRepository
{
    public class ShippingSettingRepo : IShippingSettingRepo
    {
        private readonly Context _Context;

        public ShippingSettingRepo(Context context)
        {
            _Context = context;
        }

        public async Task<int> Add(ShippingSettingVM shippingSetting)
        {
            var ShippingSettingDB = new ShippingSetting
            {
                Shipping_Type = shippingSetting.Shipping_Type,
                Shipping_Price = shippingSetting.Shipping_Price,
                Shipping_Description = shippingSetting.Shipping_Description,

            };
            await _Context.ShippingSettings.AddAsync(ShippingSettingDB);
            var result = await _Context.SaveChangesAsync();
            return result;
        }

        public async Task<int> Delete(int id)
        {
            var ShippingSettingDB = await _Context.ShippingSettings.FindAsync(id);

            if (ShippingSettingDB != null)
            {
                _Context.Entry(ShippingSettingDB).State = EntityState.Deleted;


                return await _Context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> Edit(ShippingSettingVM shippingSetting)
        {
            var ShippingSettingDB = await _Context.ShippingSettings.FindAsync(shippingSetting.Id);

            if (ShippingSettingDB != null)
            {
                _Context.Entry(ShippingSettingDB).CurrentValues.SetValues(shippingSetting);
                _Context.Entry(ShippingSettingDB).State = EntityState.Modified;

                return await _Context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public Task<List<ShippingSettingVM>> Get()
        {

            var ShippingSetting = _Context.ShippingSettings.Select(set => new ShippingSettingVM
            {
                Id = set.Id,
               Shipping_Type = set.Shipping_Type,
                Shipping_Price = set.Shipping_Price,
                Shipping_Description = set.Shipping_Description,
            }).ToListAsync();
            return ShippingSetting;
        }

        public async Task<ShippingSettingVM> GetById(int id)
        {
            var ShippingSettingDB = await _Context.ShippingSettings.FindAsync(id);
            var ShippingSetting = new ShippingSettingVM()
            {
                Id = ShippingSettingDB.Id,
                Shipping_Type = ShippingSettingDB.Shipping_Type,
                Shipping_Price = ShippingSettingDB.Shipping_Price,
                Shipping_Description = ShippingSettingDB.Shipping_Description,
            };
            return ShippingSetting;
        }
    }
}
