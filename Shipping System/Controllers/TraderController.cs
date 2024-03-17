using Microsoft.AspNetCore.Mvc;
using Shipping_System.BL.Repositories.TraderRepository;
using Shipping_System.ViewModels;

namespace Shipping_System.Controllers
{
    public class TraderController : Controller
    {
        private readonly ITraderRepo _TraderRepo;

        public TraderController(ITraderRepo traderRepo)
        {
            _TraderRepo = traderRepo;
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
