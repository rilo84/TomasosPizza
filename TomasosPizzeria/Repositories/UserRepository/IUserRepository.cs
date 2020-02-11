using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Data;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser CreateUser(RegisterViewModel model);
        Task<IdentityResult> RegisterUser(ApplicationUser user, RegisterViewModel model);
        Task<IdentityResult> SetUserRole(ApplicationUser user, string roleName);
        Task<IdentityResult> SetUserBonus(ApplicationUser user, int bonuspoints);
        Task<bool> IsAdmin(ApplicationUser user);
        Task<bool> IsPremium(ApplicationUser user);
        Task<ApplicationUser> GetUser(RegisterViewModel model);
        Task<ApplicationUser> GetUser(LoginViewModel model);
        Task<SignInResult> SignInUser(RegisterViewModel model);
        Task<SignInResult> SignInUser(LoginViewModel model);
        Task SignOutUser();
    }
}
