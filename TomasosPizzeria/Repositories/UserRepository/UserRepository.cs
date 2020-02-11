using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Data;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public Task<IdentityResult> RegisterUser(ApplicationUser user, RegisterViewModel model)
        {
            return userManager.CreateAsync(user, model.Losenord);
        }

        public Task<ApplicationUser> GetUser(RegisterViewModel model)
        {
            return userManager.FindByNameAsync(model.AnvandarNamn);
        }

        public Task<ApplicationUser> GetUser(LoginViewModel model)
        {
            return userManager.FindByNameAsync(model.AnvandarNamn);
        }

        public Task<SignInResult> SignInUser(RegisterViewModel model)
        {
            return signInManager.PasswordSignInAsync(model.AnvandarNamn, model.Losenord,false,false);
        }

        public Task<SignInResult> SignInUser(LoginViewModel model)
        {
            return signInManager.PasswordSignInAsync(model.AnvandarNamn, model.Losenord, false, false);
        }

        public ApplicationUser CreateUser(RegisterViewModel model)
        {
            return new ApplicationUser
            {
                Name = model.Namn,
                UserName = model.AnvandarNamn,
                Email = model.Email,
                PhoneNumber = model.Telefon,
                Adress = model.Gatuadress,
                PostalNumber = model.Postnr,
                City = model.Postort,
            };
        }

        public Task<IdentityResult> SetUserRole(ApplicationUser user, string roleName)
        {
            return userManager.AddToRoleAsync(user, roleName);
        }

        public Task SignOutUser()
        {
            return signInManager.SignOutAsync();
        }

        public Task<bool> IsAdmin(ApplicationUser user)
        {
            return userManager.IsInRoleAsync(user, "Admin");
        }

        public Task<bool> IsPremium(ApplicationUser user)
        {
            return userManager.IsInRoleAsync(user, "PremiumUser");
        }

        public Task<IdentityResult> SetUserBonus(ApplicationUser user, int bonuspoints)
        {
            user.BonusPoints = bonuspoints;
            return userManager.UpdateAsync(user);
        }
    }
}
