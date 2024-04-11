﻿using Microsoft.AspNetCore.Mvc;
using Shipping_System.BL.Repositories.OrderRepo;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;
using NToastNotify;

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
            return View(order);
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