using Microsoft.AspNetCore.Mvc;
using Shipping_System.BL.Repositories.EmployeeRepository;
using Shipping_System.ViewModels;

namespace Shipping_System.Controllers
{
       
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepo _EmployeeRepo;

        public EmployeeController(IEmployeeRepo employeeRepo)
        {
            _EmployeeRepo = employeeRepo;
        }

        public async Task< IActionResult> Index()
        {
            var Employees = await _EmployeeRepo.Get();
            return View(Employees);
        }

         public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Create(EmployeeVM Employee) {

            if (ModelState.IsValid)
            {
                var state = await _EmployeeRepo.Add(Employee);
                if (state.Succeeded)
                {
                    await _EmployeeRepo.AddRole();
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
        public async Task<IActionResult> Update(EmployeeUpdateVM Employee)
        {
            if (ModelState.IsValid)
            {
               
                var state = await _EmployeeRepo.Edit(Employee);
                if (state.Succeeded)
                {
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
                return Ok();
            }
            return RedirectToAction("Index");

        }
    }
}
