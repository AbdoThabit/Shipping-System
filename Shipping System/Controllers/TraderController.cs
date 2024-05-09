using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Shipping_System.BL.Repositories.RepresentativeRepository;
using Shipping_System.BL.Repositories.TraderRepository;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Shipping_System.BL.Helper;

namespace Shipping_System.Controllers
{
    [Authorize]
    public class TraderController : Controller
    {
        private readonly ITraderRepo _TraderRepo;
        private readonly IToastNotification _ToastNotification;
        private readonly IMailHelper _mailHelper;


        public TraderController(ITraderRepo traderRepo, IToastNotification toastNotification, IMailHelper mailHelper)
        {
            _TraderRepo = traderRepo;
            _ToastNotification = toastNotification;
            _mailHelper = mailHelper;
        }
        [Authorize(Policy = "viewTraderPloicy")]
        public async Task<IActionResult> Index()
        {
            var Traders = await _TraderRepo.Get();
            return View(Traders);
        }
        [Authorize(Policy = "addTraderPloicy")]
        public async Task<IActionResult> Create()
        {
            var Lists = await _TraderRepo.IncludeLists();
            return View(Lists);
        }
        [HttpPost]
        [Authorize(Policy = "addTraderPloicy")]
        public async Task<IActionResult> Create(TraderRegistrationVM Trader)
        {

            if (ModelState.IsValid)
            {
                var state = await _TraderRepo.Add(Trader);
                if (state.Succeeded)
                {
                    await _TraderRepo.AddRole();
                    await _mailHelper.WelcomeEmail(Trader.FullName, Trader.UserName, Trader.Email, Trader.Password, "ترحيب بتاجر جديد");
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
            var userE = await _TraderRepo.IncludeLists();

            userE.UserName = Trader.UserName;
            userE.Email = Trader.Email;
            userE.FullName = Trader.FullName;
            userE.Address = Trader.Address;
            userE.PhoneNumber = Trader.PhoneNumber;
            userE.Branch_Id = Trader.Branch_Id;
            userE.City_Id = Trader.City_Id;
            userE.Governate_Id = Trader.Governate_Id;
            return View(userE);
        }
        [Authorize(Policy = "editTraderPloicy")]
        public async Task<IActionResult> Update(string id)
        {
            var TraderVM = await _TraderRepo.GetById(id);
            if (TraderVM == null)
                return NotFound();

            return View(TraderVM);
        }

        [HttpPost]
        [Authorize(Policy = "editTraderPloicy")]
        public async Task<IActionResult> Update(TraderVM Trader)
        {
            if (ModelState.IsValid)
            {

                var state = await _TraderRepo.Edit(Trader);
                if (state.Succeeded)
                {
                    _ToastNotification.AddSuccessToastMessage("تم تعديل بيانات التاجر بنجاح");

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

            return View(Trader);

        }
        public async Task<IActionResult> Details(string id)
        {
            var TraderVM = await _TraderRepo.GetById(id);
            if (TraderVM == null)
                return NotFound();

            return View(TraderVM);
        }

        [Authorize(Policy = "deleteTraderPloicy")]

        public async Task<IActionResult> Delete(string Id)
        {
            var state = await _TraderRepo.Delete(Id);
            if (state.Succeeded)
            {
                _ToastNotification.AddSuccessToastMessage("تم حذف بيانات الموظف بنجاح");

                return Ok();
            }
            return RedirectToAction("Index");

        }
    }
}