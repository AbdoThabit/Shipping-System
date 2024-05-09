using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Shipping_System.BL.Repositories.GovernateRepository;
using Shipping_System.BL.Repositories.RepresentativeRepository;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shipping_System.Controllers
{
    [Authorize(Roles = "موظف")]
    public class GovernateController : Controller
    {
        private readonly IGovernateRepo _GovernateRepo;
        private readonly IToastNotification _ToastNotification;



        public GovernateController(IGovernateRepo governateRepo, IToastNotification toastNotification)
        {
            _GovernateRepo = governateRepo;
            _ToastNotification = toastNotification;
        }
        [Authorize(Policy = "viewGovernatePloicy")]
        public async Task< IActionResult> Index()
        {
            var Governates = await _GovernateRepo.Get();
            return View(Governates);
        }
        [Authorize(Policy = "addGovernatePloicy")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Policy = "addGovernatePloicy")]
        public async Task< IActionResult> Create(GovernateVM governate)
        {
            if(ModelState.IsValid)
            {
               await  _GovernateRepo.Add(governate);
                _ToastNotification.AddSuccessToastMessage("تم اضافة المحافظة بنجاح");
                return RedirectToAction("Index");

            }

            return View(governate);
        }
        [Authorize(Policy = "editGovernatePloicy")]
        public async Task< IActionResult> Update(int Id)
        { 
         var Governate= await _GovernateRepo.GetById(Id);
            return View(Governate);
        }
        [HttpPost]
        [Authorize(Policy = "editGovernatePloicy")]
        public async Task<IActionResult> Update(GovernateVM governate)
        {
            if (ModelState.IsValid)
            {
          var result = await _GovernateRepo.Edit(governate);
                if(result != 0)
                {
                    _ToastNotification.AddSuccessToastMessage("تم تعديل المحافظة بنجاح");

                    return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update governate. Please try again.");
                    return View(governate);
                }
            }
            return View(governate);
           
        }
        [Authorize(Policy = "deleteGovernatePloicy")]
        public async Task<IActionResult> Delete(int Id)
        {
          var result = await _GovernateRepo.Delete(Id);
            if (result != 0)
            {
                _ToastNotification.AddSuccessToastMessage("تم حذف المحافظة بنجاح");

                return Ok();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete governate. Please try again.");
                return RedirectToAction("Index");
            }
        }


    }
}
