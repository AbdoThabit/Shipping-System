using Microsoft.AspNetCore.Identity;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.AccountRepository
{
    public interface IAccountRepo
    {
        Task<SignInResult> Login(LoginVM Login);
        Task<ApplicationUser> FindByEmail(string Email);
        Task<string> GetToken(ApplicationUser user);
        Task<IdentityResult> ResetPassword(ApplicationUser User, string Token, string Password);
        Task<ChangeUserDataVM> GetById(string Id);
        Task<IdentityResult> ChangePassword(ApplicationUser User, string OldPassword, string NewPassword);
        Task<IdentityResult> UpdateUserData(ChangeUserDataVM User , string Id);
        Task LogOut();

    }
}
