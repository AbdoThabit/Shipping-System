using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Shipping_System.BL.Repositories.EmployeeRepository;
using Shipping_System.BL.Repositories.RolesRepository;
using Shipping_System.ViewModels;

namespace Shipping_System.Controllers
{
    public class AdminstrationController : Controller
    {
        private readonly IRolesRepo _RoleRepo;
        private readonly IEmployeeRepo _EmployeeRepo;
        private readonly IToastNotification _ToastNotification;

        public AdminstrationController(IEmployeeRepo employeeRepo, IRolesRepo roleRepo, IToastNotification toastNotification)
        {
            _EmployeeRepo = employeeRepo;
            _RoleRepo = roleRepo;
            _ToastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateEmployeesRoles()
        {
            var employees = await _EmployeeRepo.GetCheckedEmployees();
            return View("EmployeesRoles", employees);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateEmployeesRoles(List<EmployeeRolesVM> emplyeeAdminsVMs)
        {
            var result = await _EmployeeRepo.editEmployeeRole(emplyeeAdminsVMs);
            _ToastNotification.AddSuccessToastMessage("تم التعديل بنجاح");
            return RedirectToAction("UpdateEmployeesRoles", "Adminstration");

        }
        [HttpGet]
        public async Task<IActionResult> UpdateRolesPermissions(string id)
        {
            var rolesPermissionsVM = await _RoleRepo.GetRolesPermissioncs(id);

            return View("RolesPermissions", rolesPermissionsVM);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateRolesPermissions(RolesPermissioncsVM rolesPermissionsVM)
        {
            var result = await _RoleRepo.updatePermissions(rolesPermissionsVM);
            _ToastNotification.AddSuccessToastMessage("تم التعديل بنجاح");

            return RedirectToAction("UpdateRolesPermissions", "Adminstration",new { id = rolesPermissionsVM.RoleId });

        }

    }
}
