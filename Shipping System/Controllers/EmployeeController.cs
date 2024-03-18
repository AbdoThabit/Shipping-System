using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Shipping_System.BL.Repositories.EmployeeRepository;
using Shipping_System.ViewModels;

namespace Shipping_System.Controllers
{
       
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepo _EmployeeRepo;
        private readonly IToastNotification _ToastNotification;

        public EmployeeController(IEmployeeRepo employeeRepo, IToastNotification toastNotification )
        {
            _EmployeeRepo = employeeRepo;
            _ToastNotification = toastNotification;
        }

        public async Task< IActionResult> Index()
        {
            var Employees = await _EmployeeRepo.Get();
            return View(Employees);
        }

         public async Task< IActionResult> Create()
        {
            var Lists = await _EmployeeRepo.IncludeLists();
            return View(Lists);
        }
        [HttpPost]
        public async Task< IActionResult> Create(EmployeeRegistrationVM Employee) {

            if (ModelState.IsValid)
            {
                var state = await _EmployeeRepo.Add(Employee);
                if (state.Succeeded)
                {
                    await _EmployeeRepo.AddRole();
                    _ToastNotification.AddSuccessToastMessage("تم اضافة الموظف بنجاح");

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
            return View(Employee);
        }

        public async Task<IActionResult> Update(string id)
        {
            var Employee = await _EmployeeRepo.GetById(id);
            if (Employee == null)
                return NotFound();
          
            return View(Employee);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeVM Employee)
        {
            if (ModelState.IsValid)
            {
               
                var state = await _EmployeeRepo.Edit(Employee);
                if (state.Succeeded)
                {
                    _ToastNotification.AddSuccessToastMessage("تم تعديل بيانات الموظف بنجاح");

                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in state.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }


                }
            }
            return View(Employee);

        }

        public async Task<IActionResult> Delete(string Id)
        {
            var state = await _EmployeeRepo.Delete(Id);
            if (state.Succeeded)
            {
                _ToastNotification.AddSuccessToastMessage("تم مسح بيانات الموظف بنجاح");

                return Ok();
            }
            return RedirectToAction("Index");

        }
    }
}
