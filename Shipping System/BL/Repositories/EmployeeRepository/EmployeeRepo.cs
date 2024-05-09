using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping_System.DAL.Database;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;



namespace Shipping_System.BL.Repositories.EmployeeRepository
{
    public class EmployeeRepo :IEmployeeRepo
    {
     
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly UserManager<ApplicationUser> _UserManager2;
        private readonly Context _Context;

        

        private ApplicationUser User { get; set; }


        public EmployeeRepo(Context context, UserManager<ApplicationUser> userManager, UserManager<ApplicationUser> userManager2)
        {
            _UserManager = userManager;
            _Context = context;
            _UserManager2 = userManager2;
        }


        public async Task<List<EmployeeRegistrationVM>> Get()
        {
            var Employees = await _UserManager.GetUsersInRoleAsync("موظف");
            var EmployeesVM = Employees.Select(Emp => new EmployeeRegistrationVM
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

        public async Task<List<EmployeeRolesVM>> GetCheckedEmployees()
        {
            List<EmployeeRolesVM> emplyeeRolesVMs = new List<EmployeeRolesVM>();
            var Employees = await _UserManager.GetUsersInRoleAsync("موظف");
            foreach (var Employee in  Employees)
            {
                EmployeeRolesVM emplyeeRoles = new EmployeeRolesVM()
                {
                    Id = Employee.Id,
                    Name = Employee.FullName,
                    AdminSelected = await _UserManager.IsInRoleAsync(Employee, "ادمن"),
                    SalesSelected = await _UserManager.IsInRoleAsync(Employee, "سيلز"),
                    GeneralExecutiveSelected   = await _UserManager.IsInRoleAsync(Employee,"اداري عام")
                };
                emplyeeRolesVMs.Add(emplyeeRoles);
            }

            return emplyeeRolesVMs;
        }
        //public async Task<bool> isInAdminRole(ApplicationUser user)
        //{
        //    return await _UserManager.IsInRoleAsync(user, "ادمن");
        //}
        //public async Task<ApplicationUser> getUser(string id)
        //{
        //    return await _UserManager.FindByIdAsync(id);
        //}
        public async Task<IdentityResult> editEmployeeRole(List<EmployeeRolesVM> employeeAdminVMs)
        {
            IdentityResult result = null;
            foreach (var employeeAdminVM in employeeAdminVMs)
            {
                var user = await _UserManager.FindByIdAsync(employeeAdminVM.Id);
                if (user != null)
                {
                    bool hasAdminRole = await _UserManager.IsInRoleAsync(user, "ادمن");
                    bool hasSalesRole = await _UserManager.IsInRoleAsync(user, "سيلز");
                    bool hasGenerlExcutiveRole = await _UserManager.IsInRoleAsync(user, "اداري عام");
                    if  (employeeAdminVM.AdminSelected  && !hasAdminRole)
                    {
                       result = await _UserManager.AddToRoleAsync(user, "ادمن");
                    }
                    else if(!employeeAdminVM.AdminSelected && hasAdminRole)
                    {
                       result = await _UserManager.RemoveFromRoleAsync(user, "ادمن");
                    }
                    if (employeeAdminVM.SalesSelected && !hasSalesRole)
                    {
                        result = await _UserManager.AddToRoleAsync(user, "سيلز");
                    }
                    else if (!employeeAdminVM.SalesSelected && hasSalesRole)
                    {
                        result = await _UserManager.RemoveFromRoleAsync(user, "سيلز");
                    }
                    if (employeeAdminVM.GeneralExecutiveSelected && !hasGenerlExcutiveRole)
                    {
                        result = await _UserManager.AddToRoleAsync(user, "اداري عام");
                    }
                    else if (!employeeAdminVM.GeneralExecutiveSelected && hasGenerlExcutiveRole)
                    {
                        result = await _UserManager.RemoveFromRoleAsync(user, "اداري عام");
                    }
                    else
                    {
                        continue;
                    }
                     
                }
            }
            return result;
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
                UserName = Employee.UserName,
                Email = Employee.Email,
                Branch_Id = Employee.Branch_Id,
                City_Id = Employee.City_Id,
                Governate_Id = Employee.Governate_Id,
                BranchName =Employee.Branch.Name,
                CityName = Employee.City.Name,
                GovernateName = Employee.Governate.Name,
                Governates = await _Context.Governates.ToListAsync(),
                Cities = await _Context.Cities.ToListAsync(),
                Branches = await _Context.Branches.ToListAsync(),
            };
            return EmployeeVM;
        }
        public Task<IdentityResult>Add(EmployeeRegistrationVM Employee)
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

        public async Task<IdentityResult> Delete(string Id)
        {
            var user = await _UserManager.FindByIdAsync(Id);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            var result = await _UserManager.DeleteAsync(user);
            return result;
        }
        public async Task<IdentityResult> Edit(EmployeeVM Employee)
        {
            var user = await _UserManager.FindByIdAsync(Employee.Id);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found."});
            }

            user.UserName = Employee.UserName;
            user.Email = Employee.Email;
            user.FullName = Employee.FullName;
            user.Address = Employee.Address;
            user.PhoneNumber = Employee.PhoneNumber;
            user.Branch_Id = Employee.Branch_Id;
            user.City_Id = Employee.City_Id;
            user.Governate_Id = Employee.Governate_Id;

            var state = await _UserManager.UpdateAsync(user);
            return state;
        }

        public async Task<IdentityResult> AddRole()
        {
            var state = await _UserManager.AddToRoleAsync(User,"موظف");
            return state;
        }

        public async Task<EmployeeRegistrationVM> IncludeLists()
        {
            var Lists = new EmployeeRegistrationVM
            {
                Governates = await _Context.Governates.ToListAsync(),
                Cities = await _Context.Cities.ToListAsync(),
                Branches = await _Context.Branches.ToListAsync(),


            };
            return Lists;
        }


    }
}
