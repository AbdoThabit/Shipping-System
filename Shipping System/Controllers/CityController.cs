using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Shipping_System.BL.Repositories.CityRepository;
using Shipping_System.BL.Repositories.GovernateRepository;
using Shipping_System.ViewModels;

namespace Shipping_System.Controllers
{
    [Authorize]
    public class CityController : Controller
    {
        private readonly ICityRepo _CityRepo;
        private readonly IToastNotification _ToastNotification;



        public CityController(ICityRepo cityRepo, IToastNotification toastNotification)
        {
            _CityRepo = cityRepo;
            _ToastNotification = toastNotification;
        }
        [Authorize(Policy = "viewCityPloicy")]
        public async Task<IActionResult> Index()
        {
            var Cities = await _CityRepo.Get();
            return View(Cities);
        }
        [Authorize(Policy = "addCityPloicy")]
        public async Task <IActionResult> Create()
        {
            var Lists = await _CityRepo.IncludeLists();
            return View(Lists);
        }

        [HttpPost]
        [Authorize(Policy = "addCityPloicy")]
        public async Task<IActionResult> Create(CityVM city)
        {
            if (ModelState.IsValid)
            {
                await _CityRepo.Add(city);
                _ToastNotification.AddSuccessToastMessage("تم اضافة المدينة بنجاح");
                return RedirectToAction("Index");

            }

            return View(city);
        }
        [Authorize(Policy = "editCityPloicy")]
        public async Task<IActionResult> Update(int Id)
        {
            var Governate = await _CityRepo.GetById(Id);
            return View(Governate);
        }
        [HttpPost]
        [Authorize(Policy = "editCityPloicy")]
        public async Task<IActionResult> Update(CityVM city)
        {
            if (ModelState.IsValid)
            {
                var result = await _CityRepo.Edit(city);
                if (result != 0)
                {
                    _ToastNotification.AddSuccessToastMessage("تم تعديل المحافظة بنجاح");

                    return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update governate. Please try again.");
                    return View(city);
                }
            }
            return View(city);

        }
        [Authorize(Policy = "deleteCityPloicy")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _CityRepo.Delete(Id);
            if (result != 0)
            {
                _ToastNotification.AddSuccessToastMessage("تم حذف المدينة بنجاح");

                return Ok();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete governate. Please try again.");
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetCitesOfGovernate(int id)
        {
            var Cites = await _CityRepo.GetByGovernateId(id);
            return Json(Cites);
        }
    }
}
