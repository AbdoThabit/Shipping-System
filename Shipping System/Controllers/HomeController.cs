using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipping_System.BL.Repositories.OrderRepo;
using System;
using System.Diagnostics;

namespace Shipping_System.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IOrderRepo _IOrderRepo;
        public HomeController(ILogger<HomeController> logger , IOrderRepo orderRepo)
        {
            _logger = logger;
            _IOrderRepo = orderRepo;
        }

        public async Task<IActionResult> Index()
        {
            
            if (User.IsInRole("تاجر"))
            {
                    var TraderStatusCount = await _IOrderRepo.GetTraderStatusCount(User.Identity.Name);
                    return View(TraderStatusCount);
            }

            else if (User.IsInRole("مندوب"))
            {
                var RepresentiveStatusCount = await _IOrderRepo.GetRepresentiveStatusCount(User.Identity.Name);
                return View(RepresentiveStatusCount);
            }
            //if the user is not trader or representive then he must be an employee
            var statusCount = await _IOrderRepo.GetAllStatusCount();
            return View(statusCount);
        }
      


    }
}
