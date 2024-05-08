using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Shipping_System.BL.Repositories.BranchRepository;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.Controllers
{
    [Authorize]
    public class BranchController : Controller
    {
        private readonly IBranchRepo _BranchRepo;
        private readonly IToastNotification _ToastNotification;


        
        public BranchController(IBranchRepo BranchRepo, IToastNotification toastNotification)
        {
            _BranchRepo = BranchRepo;
            _ToastNotification = toastNotification;
        }
        [Authorize(Policy = "viewBranchPloicy")]
        public async Task<IActionResult> Index()
        {
            var Branches = await _BranchRepo.Get();
            return View(Branches);
        }
        [Authorize(Policy = "addBranchPloicy")]
        public async Task<IActionResult> Create()
        {
            var Lists = await _BranchRepo.IncludeLists();
            return View(Lists);
        }

        [HttpPost]
        [Authorize(Policy = "addBranchPloicy")]
        public async Task<IActionResult> Create(BranchVM Branch)
        {
            if (ModelState.IsValid)
            {
                await _BranchRepo.Add(Branch);
                _ToastNotification.AddSuccessToastMessage("تم اضافة الفرع بنجاح");
                return RedirectToAction("Index");

            }
            return View(Branch);
        }
        [Authorize(Policy = "editBranchPloicy")]
        public async Task<IActionResult> Update(int Id)
        {
            var Branch = await _BranchRepo.GetById(Id);
           

            return View(Branch);
        }
        [HttpPost]
        [Authorize(Policy = "editBranchPloicy")]
        public async Task<IActionResult> Update(BranchVM Branch)
        {
            if (ModelState.IsValid)
            {
                var result = await _BranchRepo.Edit(Branch);
                if (result != 0)
                {
                    _ToastNotification.AddSuccessToastMessage("تم تعديل الفرع بنجاح");

                    return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update governate. Please try again.");
                    return View(Branch);
                }
            }
            return View(Branch);

        }
        [Authorize(Policy = "deleteBranchPloicy")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _BranchRepo.Delete(Id);
            if (result != 0)
            {
                _ToastNotification.AddSuccessToastMessage("تم حذف الفرع بنجاح");

                return Ok();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete governate. Please try again.");
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetBranchesesOfCity(int id)
        {
            var Branches = await _BranchRepo.GetByCityId(id);
            return Json(Branches);
        }
    }
}
