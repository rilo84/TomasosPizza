using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Data;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public ApplicationUser ConvertUserInfoModelToUser(UserInfoViewModel model, ApplicationUser user)
        {

            user.Name = model.Namn;
            user.Adress = model.Gatuadress;
            user.PostalNumber = model.Postnr;
            user.City = model.Postort;
            user.Email = model.Email;
            user.PhoneNumber = model.Telefon;
   
            return user;
        }

        public UserInfoViewModel ConvertUserToUserInfoModel(ApplicationUser user)
        {
            var model = new UserInfoViewModel();

            model.Id = user.Id;
            model.Namn = user.Name;
            model.Gatuadress = user.Adress;
            model.Postnr = user.PostalNumber;
            model.Postort = user.City;
            model.Email = user.Email;
            model.Telefon = user.PhoneNumber;

            return model;

        }

    }
}
