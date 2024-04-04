using Microsoft.AspNetCore.Identity;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.AccountRepository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AccountRepo(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        
        public Task<ApplicationUser> FindByEmail(string Email)
        {
            var user = _userManager.FindByEmailAsync(Email);
            return user;
        }

        public Task<SignInResult> Login(LoginVM Login)
        {
            var state = _signInManager.PasswordSignInAsync(Login.UserName, Login.Password, Login.RememberMe, false);
            return state;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();

        }
    }
}
