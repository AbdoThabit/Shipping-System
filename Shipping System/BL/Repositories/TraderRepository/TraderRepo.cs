using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Identity;
using Shipping_System.DAL.Database;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.TraderRepository;


    public class TraderRepo : ITraderRepo
    {

            private readonly UserManager<ApplicationUser> _UserManager;
            private ApplicationUser User { get; set; }


            public TraderRepo(Context context, UserManager<ApplicationUser> userManager)
            {
                _UserManager = userManager;
            }


            public async Task<List<TraderVM>> Get()
            {
                var Traders = await _UserManager.GetUsersInRoleAsync("Trader");
                var TraderVM = Traders.Select(Trd => new TraderVM
                {
                    FullName = Trd.FullName,
                    Address = Trd.Address,
                    PhoneNumber = Trd.PhoneNumber,
                    Email = Trd.Email,
                    Branch_Id = Trd.Branch_Id,
                    City_Id = Trd.City_Id,
                    Governate_Id = Trd.Governate_Id,
                    Trader_RejOrderPrec =Trd.Trader_RejOrderPrec,
                }).ToList();

                return TraderVM;
            }

            public async Task<TraderVM> GetById(string id)
            {
                var Trader = await _UserManager.FindByIdAsync(id);
                TraderVM TraderVM = new TraderVM()
                {
                    FullName = Trader.FullName,
                    Address = Trader.Address,
                    PhoneNumber = Trader.PhoneNumber,
                    Branch_Id = Trader.Branch_Id,
                    City_Id = Trader.City_Id,
                    Governate_Id = Trader.Governate_Id,
                    Trader_RejOrderPrec = Trader.Trader_RejOrderPrec,

                };
                return TraderVM;
            }
            public Task<IdentityResult> Add(TraderVM Trader)
            {
                User = new ApplicationUser
                {
                    UserName = Trader.UserName,
                    Email = Trader.Email,
                    FullName = Trader.FullName,
                    Address = Trader.Address,
                    PhoneNumber = Trader.PhoneNumber,
                    Branch_Id = Trader.Branch_Id,
                    City_Id = Trader.City_Id,
                    Governate_Id = Trader.Governate_Id,
                    Trader_RejOrderPrec = Trader.Trader_RejOrderPrec,


                };
                var state = _UserManager.CreateAsync(User, Trader.Password);
                return state;
            }

            public async Task<IdentityResult> Delete(ApplicationUser Trader)
            {
                var state = await _UserManager.DeleteAsync(Trader);
                return state;
            }
            public async Task<IdentityResult> Edit(ApplicationUser Trader)
            {
                var state = await _UserManager.UpdateAsync(Trader);
                return state;
            }

            public async Task<IdentityResult> AddRole()
            {
                var state = await _UserManager.AddToRoleAsync(User, "Trader");
                return state;
            }
        }

