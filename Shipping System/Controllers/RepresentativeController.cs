using Microsoft.AspNetCore.Mvc;
using Shipping_System.BL.Repositories.RepresentativeRepository;
using Shipping_System.ViewModels;

namespace Shipping_System.Controllers
{
    public class RepresentativeController : Controller
    {
        private readonly IRepresentativeRepo _RepresentativeRepo;

        public RepresentativeController(IRepresentativeRepo representativeRepo)
        {
            _RepresentativeRepo = representativeRepo;
        }

        public async Task<IActionResult> Index()
        {
            var Representatives = await _RepresentativeRepo.Get();
            return View(Representatives);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RepresentativeVM Representative)
        {

            if (ModelState.IsValid)
            {
                var state = await _RepresentativeRepo.Add(Representative);
                if (state.Succeeded)
                {
                    await _RepresentativeRepo.AddRole();
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
            return View(Representative);
        }
    }
}
