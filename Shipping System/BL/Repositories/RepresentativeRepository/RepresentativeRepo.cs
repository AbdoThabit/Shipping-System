using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping_System.DAL.Database;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;
using Shipping_System.BL.Repositories.RepresentativeRepository;
using Microsoft.CodeAnalysis.Operations;
using System.Net;
using Shipping_System.BL.Helper;
using System.Text;


    public class RepresentativeRepo : IRepresentativeRepo
    {

        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly Context _Context;
    private readonly IMailHelper _mailHelper;




    private ApplicationUser User { get; set; }


    public RepresentativeRepo(Context context, UserManager<ApplicationUser> userManager, IMailHelper mailHelper)
    {
        _UserManager = userManager;
        _Context = context;
        _mailHelper = mailHelper;
    }


    public async Task<List<RepresentativeRegistrationVM>> Get()
        {
        var Representatives = await _UserManager.GetUsersInRoleAsync("مندوب");
        var RepresentativeVM = Representatives.Select(Rep => new RepresentativeRegistrationVM
        {
            Id = Rep.Id,
            FullName = Rep.FullName,
            Address = Rep.Address,
            PhoneNumber = Rep.PhoneNumber,
            Email = Rep.Email,
            Branch_Id = Rep.Branch_Id,
            City_Id = Rep.City_Id,
            Governate_Id = Rep.Governate_Id,
            type_of_discount = Rep.type_of_discount,
            company = Rep.company_percantage,
        }).ToList();

        return RepresentativeVM;
    }

    public async Task<RepresentativeVM> GetById(string id)
    {
        var Representative = await _UserManager.FindByIdAsync(id);
        RepresentativeVM RepresentativeVM = new RepresentativeVM()
        {
            Id = Representative.Id,
            FullName = Representative.FullName,
            Address = Representative.Address,
            PhoneNumber = Representative.PhoneNumber,
            UserName = Representative.UserName,
            Email = Representative.Email,
            Branch_Id = Representative.Branch_Id,
            City_Id = Representative.City_Id,
            Governate_Id = Representative.Governate_Id,
            type_of_discount = Representative.type_of_discount,
            company = Representative.company_percantage ?? Representative.company_value,
            CityName = Representative.City.Name,
            GovernateName = Representative.Governate.Name,
            BranchName = Representative.Branch.Name,
           Governates = await _Context.Governates.ToListAsync(),
           Cities = await _Context.Cities.ToListAsync(),
           Branches = await _Context.Branches.ToListAsync(),

        };
        return RepresentativeVM;
    }
        public Task<IdentityResult> Add(RepresentativeRegistrationVM Representative)
        {
        User = new ApplicationUser
        {
            UserName = Representative.UserName,
            Email = Representative.Email,
            FullName = Representative.FullName,
            Address = Representative.Address,
            PhoneNumber = Representative.PhoneNumber,
            Branch_Id = Representative.Branch_Id,
            City_Id = Representative.City_Id,
            Governate_Id = Representative.Governate_Id,
            type_of_discount = Representative.type_of_discount,
            company_value = Representative.type_of_discount == 1 ? Representative.company : null,
            company_percantage = Representative.type_of_discount != 1 ? Representative.company : null

        };

            var state = _UserManager.CreateAsync(User, Representative.Password);
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
    public async Task<IdentityResult> Edit(RepresentativeVM Representative)
    {
        var user = await _UserManager.FindByIdAsync(Representative.Id);

        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        user.UserName = Representative.UserName;
        user.Email = Representative.Email;
        user.FullName = Representative.FullName;
        user.Address = Representative.Address;
        user.PhoneNumber = Representative.PhoneNumber;
        user.Branch_Id = Representative.Branch_Id;
        user.City_Id = Representative.City_Id;
        user.Governate_Id = Representative.Governate_Id;
        user.type_of_discount = Representative.type_of_discount;
        user.company_value = Representative.type_of_discount == 1 ? Representative.company : null;
        user.company_percantage = Representative.type_of_discount != 1 ? Representative.company : null;
           
            var state = await _UserManager.UpdateAsync(user);
            return state;
        }
    
        public async Task<IdentityResult> AddRole()
        {
            var state = await _UserManager.AddToRoleAsync(User, "مندوب");
            return state;
        }

        public async Task<RepresentativeRegistrationVM> IncludeLists()
        {
            var Lists = new RepresentativeRegistrationVM
            {
                Governates = await _Context.Governates.ToListAsync(),
                Cities = await _Context.Cities.ToListAsync(),
                Branches = await _Context.Branches.ToListAsync(),

            };
            return Lists;
        }

    public async Task<List<RepresetivecheckListVm>> getAllRepresentivesOfBranch(int branchId)
    {
        var representives = await _UserManager.GetUsersInRoleAsync("مندوب");
        var representivesOfBranch = representives.Where(r => r.Branch_Id == branchId)
                                                 .Select(r => new RepresetivecheckListVm()
                                                 {
                                                     Id = r.Id,
                                                     Name = r.FullName,
                                                 })
                                                 .ToList();
        return representivesOfBranch;


    }

   
}

