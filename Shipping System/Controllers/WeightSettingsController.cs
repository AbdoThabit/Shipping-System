using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Shipping_System.BL.Repositories.WeightSettingsRepository;
using Shipping_System.ViewModels;
using Microsoft.AspNetCore.Authorization;


namespace Shipping_System.Controllers
{
    [Authorize]
    public class WeightSettingsController : Controller
    {
        private readonly IWeightSettingsRepo _WeightSettingsRepo;
        private readonly IToastNotification _ToastNotification;
        public WeightSettingsController(IWeightSettingsRepo weightSettingsRepo , IToastNotification toastNotification)
        {
            _WeightSettingsRepo = weightSettingsRepo;
            _ToastNotification = toastNotification;
        }



        //public async Task<IActionResult> Index()
        //{
        //    var ShippingSettings = await _WeightSettingsRepo.Get();
        //    return View(ShippingSettings);
        //}
        //public async Task<IActionResult> Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(WeightSettingsVM weightSettingsVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _WeightSettingsRepo.Add(weightSettingsVM);
        //        _ToastNotification.AddSuccessToastMessage(" تم اضافة اعداد الوزن");
        //        return RedirectToAction("Index");

        //    }

        //    return View(weightSettingsVM);
        //}
        [Authorize(Policy = "viewWeightSettingPloicy")]
        public async Task<IActionResult> Update()
        {
            var WeightSetting = await _WeightSettingsRepo.GetWeightSettings();
            return View(WeightSetting);
        }
        [HttpPost]
        [Authorize(Policy = "editWeightSettingPloicy")]
        public async Task<IActionResult> Update(WeightSettingsVM WeightSetting)
        {
            if (ModelState.IsValid)
            {
                var result = await _WeightSettingsRepo.Edit(WeightSetting);
                if (result != 0)
                {
                    _ToastNotification.AddSuccessToastMessage("تم حفظ اعدادات الوزن بنجاح");

                    return RedirectToAction(nameof(Update));

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update . Please try again.");
                    return View(WeightSetting);
                }
            }
            return View(WeightSetting);

        }
        //public async Task<IActionResult> Delete(int Id)
        //{
        //    var result = await _WeightSettingsRepo.Delete(Id);
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
