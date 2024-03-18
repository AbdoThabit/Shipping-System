using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Shipping_System.BL.Repositories.TraderRepository;
using Shipping_System.ViewModels;

namespace Shipping_System.Controllers
{
    public class TraderController : Controller
    {
        private readonly ITraderRepo _TraderRepo;
        private readonly IToastNotification _ToastNotification;


        public TraderController(ITraderRepo traderRepo, IToastNotification toastNotification)
        {
            _TraderRepo = traderRepo;
            _ToastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var Traders = await _TraderRepo.Get();
            return View(Traders);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TraderVM Trader)
        {

            if (ModelState.IsValid)
            {
                var state = await _TraderRepo.Add(Trader);
                if (state.Succeeded)
                {
                    await _TraderRepo.AddRole();
                    _ToastNotification.AddSuccessToastMessage("تم اضافة التاجر بنجاح");
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
            return View(Trader);
        }
    }
}
