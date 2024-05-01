using Microsoft.AspNetCore.Identity;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.AccountRepository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AccountRepo(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        
        public Task<ApplicationUser> FindByEmail(string Email)
        {
            var user = _userManager.FindByEmailAsync(Email);
            return user;
        }

        public Task<string> GetToken(ApplicationUser user)
        {
            var token = _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }

        public async Task<IdentityResult> ResetPassword(ApplicationUser User, string Token, string Password)
        {

            var result = await _userManager.ResetPasswordAsync(User, Token, Password);
            return result;
        }

        public async Task<IdentityResult> ChangePassword(ApplicationUser User, string OldPassword, string NewPassword)
        {

            var result = await _userManager.ChangePasswordAsync(User, OldPassword, NewPassword);
            return result;
        }

        public Task<SignInResult> Login(LoginVM Login)
        {
            var state = _signInManager.PasswordSignInAsync(Login.UserName, Login.Password, Login.RememberMe, false);
            return state;
        }
        public async Task<ChangeUserDataVM> GetById(string Id)
        {
          var UserDB=  await  _userManager.FindByIdAsync(Id);
            var User = new ChangeUserDataVM
            {
                FullName = UserDB.FullName,
                UserName = UserDB.UserName,
                Email = UserDB.Email,
                Address = UserDB.Address,
                PhoneNumber = UserDB.PhoneNumber
            };
            return User;
        }

        public async Task<IdentityResult> UpdateUserData(ChangeUserDataVM UserData , string Id)
        {
            
            var UserDB = await _userManager.FindByIdAsync(Id);

            if (UserDB == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            UserDB.UserName = UserData.UserName;
            UserDB.Email = UserData.Email;
            UserDB.FullName = UserData.FullName;
            UserDB.Address = UserData.Address;
            UserDB.PhoneNumber = UserData.PhoneNumber;
            var state = await _userManager.UpdateAsync(UserDB);
            return state;
        }




        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();

        }
    }
}
