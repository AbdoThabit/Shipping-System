﻿using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.VillageSettingsRepository
{
    public interface IVillageSettingRepoe
    {
        Task<List<VillageSettingVM>> Get();
        Task<int> Add(VillageSettingVM villagevm);
        Task<VillageSettingVM> GetById(int id);
        Task<int> Edit(VillageSettingVM villagevm);
        Task<int> Delete(int id);
    }
}
