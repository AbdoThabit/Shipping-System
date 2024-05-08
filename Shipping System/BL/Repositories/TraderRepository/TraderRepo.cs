using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping_System.DAL.Database;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;
using System.Text;

namespace Shipping_System.BL.Repositories.TraderRepository;


public class TraderRepo : ITraderRepo
{

    private readonly UserManager<ApplicationUser> _UserManager;
    private readonly Context _Context;

    private ApplicationUser User { get; set; }


    public TraderRepo(Context context, UserManager<ApplicationUser> userManager)
    {
        _UserManager = userManager;
        _Context = context;
    }


    public async Task<List<TraderRegistrationVM>> Get()
    {
        var Traders = await _UserManager.GetUsersInRoleAsync("تاجر");
        var TraderVM = Traders.Select(Trd => new TraderRegistrationVM
        {
            Id = Trd.Id,
            FullName = Trd.FullName,
            Address = Trd.Address,
            PhoneNumber = Trd.PhoneNumber,
            Email = Trd.Email,
            Branch_Id = Trd.Branch_Id,
            City_Id = Trd.City_Id,
            Governate_Id = Trd.Governate_Id,
            Trader_RejOrderPrec = Trd.Trader_RejOrderPrec,
        }).ToList();

        return TraderVM;
    }

    public async Task<TraderVM> GetById(string id)
    {
        var Trader = await _UserManager.FindByIdAsync(id);
        TraderVM TraderVM = new TraderVM()
        {
            Id = Trader.Id,
            FullName = Trader.FullName,
            Address = Trader.Address,
            PhoneNumber = Trader.PhoneNumber,
            Email =Trader.Email,
            UserName = Trader.UserName,
            Branch_Id = Trader.Branch_Id,
            City_Id = Trader.City_Id,
            Governate_Id = Trader.Governate_Id,
            Trader_RejOrderPrec = Trader.Trader_RejOrderPrec,
            BranchName=Trader.Branch.Name,
            GovernateName = Trader.Governate.Name,
            CityName = Trader.City.Name,
           Governates = await _Context.Governates.ToListAsync(),
            Cities = await _Context.Cities.ToListAsync(),
            Branches = await _Context.Branches.ToListAsync(),

        };
        return TraderVM;
    }
    public Task<IdentityResult> Add(TraderRegistrationVM Trader)
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
    public async Task<IdentityResult> Edit(TraderVM Trader)
    {
        var user = await _UserManager.FindByIdAsync(Trader.Id);

        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        user.UserName = Trader.UserName;
        user.Email = Trader.Email;
        user.FullName = Trader.FullName;
        user.Address = Trader.Address;
        user.PhoneNumber = Trader.PhoneNumber;
        user.Branch_Id = Trader.Branch_Id;
        user.City_Id = Trader.City_Id;
        user.Governate_Id = Trader.Governate_Id;
        user.Trader_RejOrderPrec = Trader.Trader_RejOrderPrec;

        var state = await _UserManager.UpdateAsync(user);
        return state;
    }


    public async Task<IdentityResult> AddRole()
    {
        var state = await _UserManager.AddToRoleAsync(User, "تاجر");
        return state;
    }

    public async Task<TraderRegistrationVM> IncludeLists()
    {
        var Lists = new TraderRegistrationVM
        {
            Governates = await _Context.Governates.ToListAsync(),

            Cities = await _Context.Cities.ToListAsync(),
            Branches = await _Context.Branches.ToListAsync(),


        };
        return Lists;
    }

    
}

