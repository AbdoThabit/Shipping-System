using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using NToastNotify;
using Shipping_System.BL.Repositories.RepresentativeRepository;
using Shipping_System.ViewModels;
using System.Net;

namespace Shipping_System.Controllers
{
    public class RepresentativeController : Controller
    {
        private readonly IRepresentativeRepo _RepresentativeRepo;
        private readonly IToastNotification _ToastNotification;

        public RepresentativeController(IRepresentativeRepo representativeRepo, IToastNotification toastNotification)
        {
            _RepresentativeRepo = representativeRepo;
            _ToastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var Representatives = await _RepresentativeRepo.Get();
            return View(Representatives);
        }

        public async Task<IActionResult> Create()
        {
            var Lists = await _RepresentativeRepo.IncludeLists();
            return View(Lists);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RepresentativeRegistrationVM Representative)
        {

            if (ModelState.IsValid)
            {
                var state = await _RepresentativeRepo.Add(Representative);
                if (state.Succeeded)
                {
                    await _RepresentativeRepo.AddRole();
                    _ToastNotification.AddSuccessToastMessage("تم اضافة المندوب بنجاح");

                    return RedirectToAction("Index");
                }
                else
                {

                    foreach (var Erorr in state.Errors)
                    {
                        ModelState.AddModelError("", Erorr.Description);
                    }
                }
            }
            var user = await _RepresentativeRepo.IncludeLists();
            user.UserName = Representative.UserName;
            user.Email = Representative.Email;
            user.FullName = Representative.FullName;
            user.Address = Representative.Address;
            user.PhoneNumber = Representative.PhoneNumber;
            user.Branch_Id = Representative.Branch_Id;
            user.City_Id = Representative.City_Id;
            user.Governate_Id = Representative.Governate_Id;
            user.type_of_discount = Representative.type_of_discount;
            user.company_percantage = Representative.company_percantage;

            return View(user);
        }

        public async Task<IActionResult> Update(string id)
        {
            var Representative = await _RepresentativeRepo.GetById(id);
            if (Representative == null)
                return NotFound();

            return View(Representative);
        }

        [HttpPost]        public async Task<IActionResult> Update(RepresentativeVM Representative)
        {
            if (ModelState.IsValid)
            {

                var state = await _RepresentativeRepo.Edit(Representative);
                if (state.Succeeded)
                {
                    _ToastNotification.AddSuccessToastMessage("تم تعديل بيانات المندوب بنجاح");

                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in state.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }


                }
            }
            return View(Representative);

        }

        public async Task<IActionResult> Delete(string Id)
        {
            var state = await _RepresentativeRepo.Delete(Id);
            if (state.Succeeded)
            {
                _ToastNotification.AddSuccessToastMessage("تم مسح بيانات المندوب بنجاح");

                return Ok();
            }
            return RedirectToAction("Index");

        }
    }
}
