using Microsoft.AspNetCore.Mvc;
using Shipping_System.BL.Repositories.OrderRepo;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;
using NToastNotify;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Shipping_System.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepo _OrderRepo;
        private readonly IToastNotification _ToastNotification;

        public OrderController(IOrderRepo orderRepo, IToastNotification toastNotification)
        {
            _OrderRepo = orderRepo;
            _ToastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _OrderRepo.GetAll();
            return View(orders);
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
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int Id)
        {
            var order = await _OrderRepo.GetById(Id);
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> Update(OrderVM ordervm)
        {
           
            var result = await _OrderRepo.Edit(ordervm);
            if (result != 0)
            {
                _ToastNotification.AddSuccessToastMessage("تم تعديل الطلــب بنجاح");

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to update . Please try again.");
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> UpdateStatus(int Id)
        {
            var orderStatusVm = await _OrderRepo.GetStatus(Id);
            return View(orderStatusVm);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(OrderStatusVM orderStatusVm)
        {

            var result = await _OrderRepo.updateStatus(orderStatusVm);
            if (result != 0)
            {
                _ToastNotification.AddSuccessToastMessage("تم تعديل الحالة بنجاح");

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to update . Please try again.");
                return RedirectToAction("Index");
            }
        }
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
        
    }
}
