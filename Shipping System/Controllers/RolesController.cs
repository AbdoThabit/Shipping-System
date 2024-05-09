using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Shipping_System.BL.Repositories.RolesRepository;
using Shipping_System.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Shipping_System.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly IRolesRepo _rolesRep;
        private readonly IToastNotification _ToastNotification;

       
        public RolesController(IRolesRepo rolesRep, IToastNotification toastNotification)
        {
            _rolesRep = rolesRep;
            _ToastNotification = toastNotification;
        }

        [Authorize(Policy = "viewRolePloicy")]
        public async Task<IActionResult> Index()
        {
            var Roles = await _rolesRep.GetRoles();
            return View(Roles);
        }
        [Authorize(Policy = "editRolePloicy")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Policy = "addRolePloicy")]
        public async Task<IActionResult> Create(RolesVM role)
        {
            if (ModelState.IsValid)
            {
                var state = await _rolesRep.CreateRole(role);
                if (state.Succeeded)
                {
                    _ToastNotification.AddSuccessToastMessage("تم اضافة الصلاحية بنجاح");
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


            return View(role);
        }
        [Authorize(Policy = "editRolePloicy")]
        public async Task<IActionResult> Update(string id)
        {
            var roleDB = await _rolesRep.GetRoleById(id);
            if (roleDB == null)
                return NotFound();
            var role = new RolesVM
            {
                Id = roleDB.Id,
                Name = roleDB.Name

            };
            return View(role);
        }
        [HttpPost]
        [Authorize(Policy = "editRolePloicy")]
        public async Task<IActionResult> Update(RolesVM role)
        {
            if (ModelState.IsValid)
            {
                var RoleDB = await _rolesRep.GetRoleById(role.Id);
                RoleDB.Name = role.Name;
                var state = await _rolesRep.UpdateRole(RoleDB);
                if (state.Succeeded)
                {
                    _ToastNotification.AddSuccessToastMessage("تم تحديث الصلاحية بنجاح");
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
            return View(role);

        }

        [Authorize(Policy = "deleteRolePloicy")]
        public async Task<IActionResult> Delete(string Id)
        {
            var role = await _rolesRep.GetRoleById(Id);
            var state = await _rolesRep.DeleteRole(Id, role);
            if (state.Succeeded)
            {

                return Ok();
            }
            return RedirectToAction("Index");

        }
    }
}
