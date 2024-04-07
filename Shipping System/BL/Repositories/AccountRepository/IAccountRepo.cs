using Microsoft.AspNetCore.Identity;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.AccountRepository
{
    public interface IAccountRepo
    {
        Task<SignInResult> Login(LoginVM Login);
        Task<ApplicationUser> FindByEmail(string Email);

        Task LogOut();

    }
}
