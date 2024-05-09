using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Shipping_System.BL.Repositories.CityRepository;
using Shipping_System.BL.Repositories.ShippingSettingRepository;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.Controllers
{
    [Authorize(Roles = "موظف")]
    public class ShippingSettingController : Controller
    {
        private readonly IShippingSettingRepo _ShippingSettingRepo;
        private readonly IToastNotification _ToastNotification;

        
        public ShippingSettingController(IShippingSettingRepo shippingSettingRepo, IToastNotification toastNotification)
        {
            _ShippingSettingRepo = shippingSettingRepo;
            _ToastNotification = toastNotification;
        }
        [Authorize(Policy = "viewShippingSettingPloicy")]
        public async Task< IActionResult> Index()
        {
            var ShippingSettings = await _ShippingSettingRepo.Get();
            return View(ShippingSettings);
        }
        [Authorize(Policy = "addShippingSettingPloicy")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "addShippingSettingPloicy")]
        public async Task<IActionResult> Create(ShippingSettingVM ShippingSetting)
        {
            if (ModelState.IsValid)
            {
                await _ShippingSettingRepo.Add(ShippingSetting);
                _ToastNotification.AddSuccessToastMessage(" تم اضافة نوع الشحن");
                return RedirectToAction("Index");

            }

            return View(ShippingSetting);
        }
        [Authorize(Policy = "editShippingSettingPloicy")]
        public async Task<IActionResult> Update(int Id)
        {
            var ShippingSetting = await _ShippingSettingRepo.GetById(Id);
            return View(ShippingSetting);
        }
        [HttpPost]
        [Authorize(Policy = "editShippingSettingPloicy")]
        public async Task<IActionResult> Update(ShippingSettingVM ShippingSetting)
        {
            if (ModelState.IsValid)
            {
                var result = await _ShippingSettingRepo.Edit(ShippingSetting);
                if (result != 0)
                {
                    _ToastNotification.AddSuccessToastMessage("تم تعديل نوع الشحن بنجاح");

                    return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update . Please try again.");
                    return View(ShippingSetting);
                }
            }
            return View(ShippingSetting);

        }
        [Authorize(Policy = "deleteShippingSettingPloicy")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _ShippingSettingRepo.Delete(Id);
            if (result != 0)
            {
                _ToastNotification.AddSuccessToastMessage("تم حذف نوع الشحن بنجاح");

                return Ok();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete . Please try again.");
                return RedirectToAction("Index");
            }
        }
    }
}
