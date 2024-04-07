using Microsoft.AspNetCore.Mvc;
using Shipping_System.BL.Repositories.OrderRepo;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepo _OrderRepo;

        public OrderController(IOrderRepo orderRepo)
        {
            _OrderRepo = orderRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task< IActionResult> Create()
        {
            var Lists = await _OrderRepo.IncludeLists();
            return View(Lists);
        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderVM order)
        {
           var result=  await _OrderRepo.Add(order);
            return View(order);
        }
    }
}
