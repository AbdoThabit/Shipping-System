using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping_System.DAL.Database;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;
using Shipping_System.BL.Repositories.RepresentativeRepository;
using Microsoft.CodeAnalysis.Operations;
using System.Net;


    public class RepresentativeRepo : IRepresentativeRepo
    {

        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly Context _Context;



        private ApplicationUser User { get; set; }


        public RepresentativeRepo(Context context, UserManager<ApplicationUser> userManager)
        {
            _UserManager = userManager;
            _Context = context;
        }


        public async Task<List<RepresentativeRegistrationVM>> Get()
        {
        var Representatives = await _UserManager.GetUsersInRoleAsync("Representative");
        var RepresentativeVM = Representatives.Select(Rep => new RepresentativeRegistrationVM
        {
            FullName = Rep.FullName,
            Address = Rep.Address,
            PhoneNumber = Rep.PhoneNumber,
            Email = Rep.Email,
            Branch_Id = Rep.Branch_Id,
            City_Id = Rep.City_Id,
            Governate_Id = Rep.Governate_Id,
            type_of_discount = Rep.type_of_discount,
            company_percantage = Rep.company_percantage,
        }).ToList();

        return RepresentativeVM;
    }

    public async Task<RepresentativeVM> GetById(string id)
    {
        var Representative = await _UserManager.FindByIdAsync(id);
        RepresentativeVM RepresentativeVM = new RepresentativeVM()
        {
            FullName = Representative.FullName,
            Address = Representative.Address,
            PhoneNumber = Representative.PhoneNumber,
            Branch_Id = Representative.Branch_Id,
            City_Id = Representative.City_Id,
            Governate_Id = Representative.Governate_Id,
            type_of_discount = Representative.type_of_discount,
            company_percantage = Representative.company_percantage,

            //Governates = await _Context.Governates.ToListAsync(),
            //Cities = await _Context.Cities.ToListAsync(),
            //Branches = await _Context.Branches.ToListAsync(),

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
                company_percantage = Representative.company_percantage,



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
        user.company_percantage = Representative.company_percantage;

            var state = await _UserManager.UpdateAsync(user);
            return state;
        }
    
        public async Task<IdentityResult> AddRole()
        {
            var state = await _UserManager.AddToRoleAsync(User, "Representative");
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


    }

