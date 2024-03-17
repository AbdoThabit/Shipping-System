using Microsoft.AspNetCore.Identity;
using Shipping_System.DAL.Database;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.RepresentativeRepository
{
    public class RepresentativeRepo :IRepresentativeRepo
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private ApplicationUser User { get; set; }


        public RepresentativeRepo(Context context, UserManager<ApplicationUser> userManager)
        {
            _UserManager = userManager;
        }


        public async Task<List<RepresentativeVM>> Get()
        {
            var Representatives = await _UserManager.GetUsersInRoleAsync("Representative");
            var RepresentativeVM = Representatives.Select(Rep => new RepresentativeVM
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

            };
            return RepresentativeVM;
        }
        public Task<IdentityResult> Add(RepresentativeVM Representative)
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

        public async Task<IdentityResult> Delete(ApplicationUser Representative)
        {
            var state = await _UserManager.DeleteAsync(Representative);
            return state;
        }
        public async Task<IdentityResult> Edit(ApplicationUser Representative)
        {
            var state = await _UserManager.UpdateAsync(Representative);
            return state;
        }

        public async Task<IdentityResult> AddRole()
        {
            var state = await _UserManager.AddToRoleAsync(User, "Representative");
            return state;
        }

        
        }
    }

