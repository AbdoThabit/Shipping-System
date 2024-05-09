using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Shipping_System.BL.Repositories.VillageSettingsRepository;
using Shipping_System.BL.Repositories.WeightSettingsRepository;
using Shipping_System.ViewModels;
using Microsoft.AspNetCore.Authorization;


namespace Shipping_System.Controllers
{
    [Authorize(Roles = "موظف")]
    public class VillageSettingController : Controller
    {
        private readonly IVillageSettingRepoe _VillageSettingsRepo;
        private readonly IToastNotification _ToastNotification;
        public VillageSettingController(IVillageSettingRepoe VillageSettingRepoe, IToastNotification toastNotification)
        {
            _VillageSettingsRepo = VillageSettingRepoe;
            _ToastNotification = toastNotification;
        }
        //public async Task<IActionResult> Index()
        //{
        //    var VillageSettings = await _VillageSettingsRepo.Get();
        //    return View(VillageSettings);
        //}
        //public async Task<IActionResult> Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(VillageSettingVM villageSettingVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _VillageSettingsRepo.Add(villageSettingVM);
        //        _ToastNotification.AddSuccessToastMessage(" تم اضافة اعداد القرية");
        //        return RedirectToAction("Index");

        //    }

        //    return View(villageSettingVM);
        //}
        [Authorize(Policy = "viewvillageSettingPloicy")]
        public async Task<IActionResult> Update()
        {
            var VillageSetting = await _VillageSettingsRepo.GetVillageSettings();
            return View(VillageSetting);
        }
        [HttpPost]
        [Authorize(Policy = "editvillageSettingPloicy")]
        public async Task<IActionResult> Update(VillageSettingVM villageSetting)
        {
            if (ModelState.IsValid)
            {
                var result = await _VillageSettingsRepo.Edit(villageSetting);
                if (result != 0)
                {
                    _ToastNotification.AddSuccessToastMessage("تم تعديل اعدادات الشحن لقرية بنجاح");

                    return RedirectToAction(nameof(Update));

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update . Please try again.");
                    return View(villageSetting);
                }
            }
            return View(villageSetting);

        }
        //public async Task<IActionResult> Delete(int Id)
        //{
        //    var result = await _VillageSettingsRepo.Delete(Id);
        //    if (result != 0)
        //    {
        //        _ToastNotification.AddSuccessToastMessage("تم حذف نوع الشحن بنجاح");

        //        return Ok();
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Failed to delete . Please try again.");
        //        return RedirectToAction("Index");
        //    }
        //}
    }
}
