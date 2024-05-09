using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using NToastNotify;
using Shipping_System.BL.Helper;
using Shipping_System.BL.Repositories.RepresentativeRepository;
using Shipping_System.ViewModels;
using System.Net;
using System.Text;

namespace Shipping_System.Controllers
{
    [Authorize]
    public class RepresentativeController : Controller
    {
        private readonly IRepresentativeRepo _RepresentativeRepo;
        private readonly IToastNotification _ToastNotification;
        private readonly IMailHelper _mailHelper;


        public RepresentativeController(IRepresentativeRepo representativeRepo, IToastNotification toastNotification, IMailHelper mailHelper)
        {
            _RepresentativeRepo = representativeRepo;
            _ToastNotification = toastNotification;
            _mailHelper = mailHelper;
        }
        [Authorize(Policy = "viewRepresentivePloicy")]
        public async Task<IActionResult> Index()
        {
            var Representatives = await _RepresentativeRepo.Get();
            return View(Representatives);
        }
        [Authorize(Policy = "addRepresentivePloicy")]
        public async Task<IActionResult> Create()
        {
            var Lists = await _RepresentativeRepo.IncludeLists();
            return View(Lists);
        }
        [HttpPost]
        [Authorize(Policy = "addRepresentivePloicy")]
        public async Task<IActionResult> Create(RepresentativeRegistrationVM Representative)
        {

            if (ModelState.IsValid)
            {
                var state = await _RepresentativeRepo.Add(Representative);
                if (state.Succeeded)
                {
                    await _RepresentativeRepo.AddRole();
                    await _mailHelper.WelcomeEmail(Representative.FullName, Representative.UserName , Representative.Email , Representative.Password, "ترحيب بمندوب جديد");
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
            user.company = Representative.company;

            return View(user);
        }
        [Authorize(Policy = "editRepresentivePloicy")]
        public async Task<IActionResult> Update(string id)
        {
            var Representative = await _RepresentativeRepo.GetById(id);
            if (Representative == null)
                return NotFound();

            return View(Representative);
        }

        [HttpPost]
        [Authorize(Policy = "editRepresentivePloicy")]
        public async Task<IActionResult> Update(RepresentativeVM Representative)
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

        public async Task<IActionResult> Details(string id)
        {
            var Representative = await _RepresentativeRepo.GetById(id);
            if (Representative == null)
                return NotFound();

            return View(Representative);
        }

        [Authorize(Policy = "deleteRepresentivePloicy")]

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
        [HttpPost]
        public async Task<IActionResult> GetRepresentivesOfbranch(int id)
        {
            var representives = await _RepresentativeRepo.getAllRepresentivesOfBranch(id);
            return Json(representives);
        }
    }
}
