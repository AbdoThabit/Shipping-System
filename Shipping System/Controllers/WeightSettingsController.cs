using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Shipping_System.BL.Repositories.WeightSettingsRepository;
using Shipping_System.ViewModels;

namespace Shipping_System.Controllers
{
    public class WeightSettingsController : Controller
    {
        private readonly IWeightSettingsRepo _WeightSettingsRepo;
        private readonly IToastNotification _ToastNotification;
        public WeightSettingsController(IWeightSettingsRepo weightSettingsRepo , IToastNotification toastNotification)
        {
            _WeightSettingsRepo = weightSettingsRepo;
            _ToastNotification = toastNotification;
        }

        

        public async Task<IActionResult> Index()
        {
            var ShippingSettings = await _WeightSettingsRepo.Get();
            return View(ShippingSettings);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WeightSettingsVM weightSettingsVM)
        {
            if (ModelState.IsValid)
            {
                await _WeightSettingsRepo.Add(weightSettingsVM);
                _ToastNotification.AddSuccessToastMessage(" تم اضافة اعداد الوزن");
                return RedirectToAction("Index");

            }

            return View(weightSettingsVM);
        }
    }
}
