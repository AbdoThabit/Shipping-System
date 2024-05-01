
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NToastNotify;
using Shipping_System.BL.Helper;
using Shipping_System.BL.hub;
using Shipping_System.BL.Repositories.AccountRepository;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;
using System.Text;

namespace Shipping_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepo _AccountRepo;
        private readonly IToastNotification _ToastNotification;
        private readonly IMailHelper _mailHelper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHubContext<NotifiactionHub> _HubContext;





        public AccountController(IAccountRepo accountRepo, IToastNotification toastNotification, IMailHelper mailHelper, UserManager<ApplicationUser> userManager, IHubContext<NotifiactionHub> hubContext)
        {
            _AccountRepo = accountRepo;
            _ToastNotification = toastNotification;
            _mailHelper = mailHelper;
            _userManager = userManager;
            _HubContext = hubContext;
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
                HttpContext.Session.SetString("Username", Login.UserName);
                if (state.Succeeded)
                {
                    _ToastNotification.AddSuccessToastMessage($"مرحبًا {Login.UserName} في لوحة التحكم");
                 await _HubContext.Clients.All.SendAsync("Online", Login.UserName);
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
            await _HubContext.Clients.All.SendAsync("Offline", User.Identity.Name);
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM ForgetPassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _AccountRepo.FindByEmail(ForgetPassword.Email);

                if (user != null)
                {
                    var token = await _AccountRepo.GetToken(user);
                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = ForgetPassword.Email, Token = token }, Request.Scheme);
                    var bodyBuilder = new StringBuilder();
                    bodyBuilder.AppendLine("<html>");
                    bodyBuilder.AppendLine("<body>");
                    bodyBuilder.AppendLine("<h1>إعادة تعيين كلمة المرور</h1>");
                    bodyBuilder.AppendLine("<p>عزيزي المستخدم،</p>");
                    bodyBuilder.AppendLine("<p>لقد تلقينا طلبًا لإعادة تعيين كلمة المرور الخاصة بك. انقر على الرابط أدناه لإعادة تعيينها:</p>");
                    bodyBuilder.AppendLine("<p><a href=\"" + passwordResetLink + "\">إعادة تعيين كلمة المرور</a></p>");
                    bodyBuilder.AppendLine("<p>إذا لم تكن قد طلبت إعادة تعيين كلمة المرور، يرجى تجاهل هذا البريد الإلكتروني.</p>");
                    bodyBuilder.AppendLine("<p>اطيب التحيات,<br>فريق بايونيرز اكسبريس </p>");
                    bodyBuilder.AppendLine("</body>");
                    bodyBuilder.AppendLine("</html>");
                    await _mailHelper.SendMail(ForgetPassword.Email, "اعادة تعيين كلمة المرور", bodyBuilder.ToString());
                    _ToastNotification.AddSuccessToastMessage("تم ارسال رابط اعادة تعيين كلمة المرور الي بريدك الالكتروني بنجاح");

                    return RedirectToAction("Login");

                }
                else
                {
                    ModelState.AddModelError("", "ايميل غير صالح");
                }

            }
            return View(ForgetPassword);
        }

        public IActionResult ResetPassword(string Email, string Token)
        {
            if (Email == null || Token == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM ResetPassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _AccountRepo.FindByEmail(ResetPassword.Email);
                if (user != null)
                {
                    var state = await _AccountRepo.ResetPassword(user, ResetPassword.Token, ResetPassword.Password);
                    if (state.Succeeded)
                    {
                        _ToastNotification.AddSuccessToastMessage("تم اعادة تعيين كلمة المرور بنجاح ");
                        return RedirectToAction("Login");
                    }
                    foreach (var error in state.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(ResetPassword);

                }

            }
            return View(ResetPassword);
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> ChangePassword(ChangePasswordVM changePassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

       
                var result = await _AccountRepo.ChangePassword(user , changePassword.OldPassword, changePassword.NewPassword);

                if (result.Succeeded)
                {
                    _ToastNotification.AddSuccessToastMessage("تم نغيير كلمة المرور بنجاح يرجي اعادة تسجيل الدخول ");

                    await _AccountRepo.LogOut();
                    return RedirectToAction("Login");

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(changePassword);
        }

        public async Task<IActionResult> ChangeUserData()
        {
            var Id =  _userManager.GetUserId(User);
            var UserDB = await _AccountRepo.GetById(Id);
            return View(UserDB);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeUserData(ChangeUserDataVM UserData)
        {
            if (ModelState.IsValid)
            {
                var Id = _userManager.GetUserId(User);
                var state = await _AccountRepo.UpdateUserData(UserData, Id);
                if (state.Succeeded)
                {
                    _ToastNotification.AddSuccessToastMessage("تم تعديل بياناتك  بنجاح");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in state.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }


                }
            }
            return View(User);


        }
    }
}
