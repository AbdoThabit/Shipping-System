using Microsoft.EntityFrameworkCore;
using Shipping_System.DAL.Database;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.VillageSettingsRepository
{
    public class VillageSettingRepoe : IVillageSettingRepoe
    {
        private readonly Context _Context;

        public VillageSettingRepoe(Context context)
        {
            _Context = context;
        }

        //public async Task<int> Add(VillageSettingVM villagevm)
        //{
        //    var villageSetting = new VillageShipping()
        //    {
        //        Price = villagevm.Price,
        //    };
        //    await _Context.VillageSettings.AddAsync(villageSetting);
        //    var result = await _Context.SaveChangesAsync();
        //    return result;
        //}

        //public async Task<int> Delete(int id)
        //{
        //    var villagetSettingDB = await _Context.VillageSettings.FindAsync(id);

        //    if (villagetSettingDB != null)
        //    {
        //        _Context.Entry(villagetSettingDB).State = EntityState.Deleted;


        //        return await _Context.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        public async Task<int> Edit(VillageSettingVM villagevm)
        {
            var villageSettingDB = await _Context.VillageSettings.SingleOrDefaultAsync();

            if (villageSettingDB != null)
            {
                _Context.Entry(villageSettingDB).CurrentValues.SetValues(villagevm);
                _Context.Entry(villageSettingDB).State = EntityState.Modified;

                return await _Context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        //public Task<List<VillageSettingVM>> Get()
        //{
        //    var villageSettings = _Context.VillageSettings.Select(set => new VillageSettingVM
        //    {
        //        Id = set.Id,
        //        Price = set.Price,
        //    }).ToListAsync();
        //    return villageSettings;
        //}

        public async Task<VillageSettingVM> GetVillageSettings()
        {
            var villageSettingDB = await _Context.VillageSettings.SingleOrDefaultAsync();
            var villageSetting = new VillageSettingVM()
            {
                Price = villageSettingDB.Price,
            };
            return villageSetting;
        }
    }
}
