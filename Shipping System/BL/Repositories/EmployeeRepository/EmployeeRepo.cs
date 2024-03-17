using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping_System.DAL.Database;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;
using System.Threading.Tasks;


namespace Shipping_System.BL.Repositories.EmployeeRepository
{
    public class EmployeeRepo :IEmployeeRepo
    {
     
        private readonly UserManager<ApplicationUser> _UserManager;
        private ApplicationUser User { get; set; }


        public EmployeeRepo(Context context, UserManager<ApplicationUser> userManager)
        {
            _UserManager = userManager;
        }


        public async Task<List<EmployeeVM>> Get()
        {
            var Employees = await _UserManager.GetUsersInRoleAsync("Employee");
            var EmployeesVM = Employees.Select(Emp => new EmployeeVM
            {
                Id = Emp.Id,
                FullName = Emp.FullName,
                Address = Emp.Address,
                PhoneNumber = Emp.PhoneNumber,
                Email = Emp.Email,
                Branch_Id = Emp.Branch_Id,
                City_Id = Emp.City_Id,
                Governate_Id = Emp.Governate_Id,
            }).ToList();

            return EmployeesVM;
        }

        public async Task<EmployeeVM> GetById(string id)
        {
            var Employee = await _UserManager.FindByIdAsync(id);
            EmployeeVM EmployeeVM = new EmployeeVM()
            {
                Id = Employee.Id,
                FullName = Employee.FullName,
                Address = Employee.Address,
                PhoneNumber = Employee.PhoneNumber,
                Branch_Id = Employee.Branch_Id,
                City_Id = Employee.City_Id,
                Governate_Id = Employee.Governate_Id,
            };
            return EmployeeVM;
        }
        public Task<IdentityResult>Add(EmployeeVM Employee)
        {
             User = new ApplicationUser
            {
                UserName = Employee.UserName,
                Email = Employee.Email,
                FullName=Employee.FullName,
                Address= Employee.Address,
                PhoneNumber=Employee.PhoneNumber,
                Branch_Id = Employee.Branch_Id,
                City_Id = Employee.City_Id,
                Governate_Id= Employee.Governate_Id,
                

            };
            var state = _UserManager.CreateAsync(User, Employee.Password);
            return state;
        }

        public async Task<IdentityResult> Delete(ApplicationUser Employee)
        {
            var state = await _UserManager.DeleteAsync(Employee);
            return state;
        }
        public async Task<IdentityResult> Edit(ApplicationUser Employee)
        {
            var state = await _UserManager.UpdateAsync(Employee);
            return state;
        }

        public async Task<IdentityResult> AddRole()
        {
            var state = await _UserManager.AddToRoleAsync(User,"Employee");
            return state;
        }


    }
}
