using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Shipping_System.BL.Repositories.EmployeeRepository;
using Shipping_System.ViewModels;

namespace Shipping_System.Controllers
{
    public class AdminstrationController : Controller
    {
        private readonly IEmployeeRepo _EmployeeRepo;
        private readonly IToastNotification _ToastNotification;

        public AdminstrationController(IEmployeeRepo employeeRepo, IToastNotification toastNotification)
        {
            _EmployeeRepo = employeeRepo;
            _ToastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateAdminsGroup()
        {
            var employees = await _EmployeeRepo.GetCheckedEmployees();
            return View("Admins", employees);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateAdminsGroup(List<EmployeeRolesVM> emplyeeAdminsVMs)
        {
            var result = await _EmployeeRepo.editAdminRole(emplyeeAdminsVMs);
            _ToastNotification.AddSuccessToastMessage("تم التعديل بنجاح");
            return RedirectToAction("UpdateAdminsGroup", "Adminstration");

        }
       
       
    }
}
