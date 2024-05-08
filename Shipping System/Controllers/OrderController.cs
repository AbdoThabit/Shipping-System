using Microsoft.AspNetCore.Mvc;
using Shipping_System.BL.Repositories.OrderRepo;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;
using NToastNotify;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Shipping_System.BL.hub;
using Microsoft.AspNetCore.Identity;

namespace Shipping_System.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepo _OrderRepo;
        private readonly IToastNotification _ToastNotification;
        private readonly IHubContext<NotifiactionHub> _HubContext;
        private readonly UserManager<ApplicationUser> _userManager;



        public OrderController(IOrderRepo orderRepo, IToastNotification toastNotification, IHubContext<NotifiactionHub> hubContext, UserManager<ApplicationUser> userManager)
        {
            _OrderRepo = orderRepo;
            _ToastNotification = toastNotification;
            _HubContext = hubContext;
            _userManager = userManager;
        }
        [Authorize(Policy = "viewOrderPloicy")]
        public async Task<IActionResult> Index()
        {
            var orders = await _OrderRepo.GetAll();
            return View(orders);
        }

        [Authorize(Policy = "viewOrderPloicy")]
        public async Task<IActionResult> TraderOrders(string UserName)
        {
            var orders = await _OrderRepo.GetTraderOrders(UserName);
            return View(orders);
        }
        [Authorize(Policy = "viewOrderPloicy")]
        public async Task<IActionResult> RepresntiveOrders(string UserName)
        {
            var orders = await _OrderRepo.GetRepresntiveOrders(UserName);
            return View(orders);
        }
        [Authorize(Policy = "viewOrderDetailsPloicy")]
        public async Task<IActionResult> ShowDetails(int Id)
        {
            var order = await _OrderRepo.GetById(Id);
            return View(order);
        }
        [Authorize(Policy = "addOrderPloicy")]
        public async Task<IActionResult> Create()
        {
            var Lists = await _OrderRepo.IncludeLists();
            return View(Lists);
        }
        [HttpPost]

        [Authorize(Policy = "addOrderPloicy")]
        public async Task<IActionResult> Create(OrderVM ordervm)
        {
            var result = await _OrderRepo.Add(ordervm);
            if (result != 0)
            {
                _ToastNotification.AddSuccessToastMessage("تم اضافة الطلــب بنجاح");
                var rep= await _userManager.FindByIdAsync(ordervm.Representitive_Id);
               await _HubContext.Clients.User(ordervm.Representitive_Id).SendAsync("Notifications",ordervm.Client_Name,rep.UserName);
                if (User.IsInRole("موظف"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("TraderOrders", new { UserName = User.Identity.Name });
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to create . Please try again.");
                if (User.IsInRole("موظف"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("TraderOrders", new { UserName = User.Identity.Name });
                }
            }
            
        }
        [Authorize(Policy = "editOrderPloicy")]
        public async Task<IActionResult> Update(int Id)
        {
            var order = await _OrderRepo.GetById(Id);
            return View(order);
        }
        [HttpPost]
        [Authorize(Policy = "editOrderPloicy")]
        public async Task<IActionResult> Update(OrderVM ordervm)
        {

            var result = await _OrderRepo.Edit(ordervm);
            if (result != 0)
            {
                _ToastNotification.AddSuccessToastMessage("تم تعديل الطلــب بنجاح");
                if (User.IsInRole("موظف"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("TraderOrders", new { UserName = User.Identity.Name });
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to update . Please try again.");
                if (User.IsInRole("موظف"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("TraderOrders", new { UserName = User.Identity.Name });
                }
            }
        }
        
        [Authorize(Policy = "editOrderStatusPloicy")]
        public async Task<IActionResult> UpdateStatus(int Id)
        {
            var orderStatusVm = await _OrderRepo.GetStatus(Id);
            return View(orderStatusVm);
        }
        [HttpPost]
        [Authorize]
        [Authorize(Policy = "editOrderStatusPloicy")]
        public async Task<IActionResult> UpdateStatus(OrderStatusVM orderStatusVm)
        {

            var result = await _OrderRepo.updateStatus(orderStatusVm);
            if (result != 0)
            {
                _ToastNotification.AddSuccessToastMessage("تم تعديل الحالة بنجاح");

                if (User.IsInRole("موظف"))
                {
                    return RedirectToAction("Index");
                }
                else if (User.IsInRole("تاجر"))
                {
                    return RedirectToAction("TraderOrders", new { UserName = User.Identity.Name });
                }
                else 
                {
                    return RedirectToAction("RepresntiveOrders", new { UserName = User.Identity.Name });
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to update . Please try again.");
                return RedirectToAction("Index");
            }
        }
        [Authorize(Policy = "deleteOrderPloicy")]

        public async Task<IActionResult> Delete(int id)
        {

            var result = await _OrderRepo.Delete(id);
            if (result != 0)
            {
                _ToastNotification.AddSuccessToastMessage("تم حذف الطلــب بنجاح");

                return Ok();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete . Please try again.");
                return RedirectToAction("Index");
            }
        }
        [Authorize(Policy = "viewOrderReportPloicy")]
        public  IActionResult Report()
        {
           return View();
        }
        [HttpPost]
        [Authorize(Policy = "viewOrderReportPloicy")]
        public async Task<IActionResult> Report(DateTime fromDate, DateTime toDate , string UserName)
        {
            var order = await _OrderRepo.GetOrdersByDateRange(fromDate, toDate, UserName);
            return View(order);
        }

    }
}
