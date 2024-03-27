using Microsoft.EntityFrameworkCore;
using Shipping_System.DAL.Database;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.WeightSettingsRepository
{
    public class WeightSettingsRepo : IWeightSettingsRepo
    {
        private readonly Context _Context;

        public WeightSettingsRepo(Context context)
        {
            _Context = context;
        }
        public async Task<int> Add(WeightSettingsVM weightvm)
        {
            var weightSetting = new WeightSetting()
            {
                Default_Weight = weightvm.Default_Weight,
                Extra_Weight = weightvm.Extra_Weight,
            };
            await _Context.WeightSettings.AddAsync(weightSetting);
            var result = await _Context.SaveChangesAsync();
            return result;
        }

        public async Task<int> Delete(int id)
        {
            var WeightSettingDB = await _Context.WeightSettings.FindAsync(id);

            if (WeightSettingDB != null)
            {
                _Context.Entry(WeightSettingDB).State = EntityState.Deleted;


                return await _Context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> Edit(WeightSettingsVM weightvm)
        {
            var WeightSettingDB = await _Context.WeightSettings.SingleOrDefaultAsync();

            if (WeightSettingDB != null)
            {
                _Context.Entry(WeightSettingDB).CurrentValues.SetValues(weightvm);
                _Context.Entry(WeightSettingDB).State = EntityState.Modified;

                return await _Context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public Task<List<WeightSettingsVM>> Get()
        {
            var WeightSettings = _Context.WeightSettings.Select(set => new WeightSettingsVM
            {
               Default_Weight = set.Default_Weight,
               Extra_Weight = set.Extra_Weight,
            }).ToListAsync();
            return WeightSettings;
        }

        public async Task<WeightSettingsVM> GetWeightSettings()
        {
            var WeightSettingDB = await _Context.WeightSettings.SingleOrDefaultAsync();
            var weightSetting = new WeightSettingsVM()
            {
                Default_Weight = WeightSettingDB.Default_Weight,
                Extra_Weight = WeightSettingDB.Extra_Weight,
            };
            return weightSetting;
        }
    }
}
