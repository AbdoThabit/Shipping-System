
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Shipping_System.BL.Repositories.AccountRepository;
using Shipping_System.ViewModels;

namespace Shipping_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepo _AccountRepo;
        private readonly IToastNotification _ToastNotification;


        public AccountController(IAccountRepo accountRepo, IToastNotification toastNotification)
        {
            _AccountRepo = accountRepo;
            _ToastNotification = toastNotification;
        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM Login)
        {
            if (ModelState.IsValid)
            {
                var state = await _AccountRepo.Login(Login);
               HttpContext.Session.SetString("Username",Login.UserName);
                if (state.Succeeded)
                {
                    _ToastNotification.AddSuccessToastMessage($"مرحبًا {Login.UserName} في لوحة التحكم");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "اسم المستخدم أو كلمة المرور غير صالحة");
                }
            }
            return View(Login);
        }
        public async Task<IActionResult> LogOut()
        {
            await _AccountRepo.LogOut();
            return RedirectToAction("Login");
        }
    }
}
