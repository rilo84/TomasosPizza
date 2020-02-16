using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeria.Data;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Services
{
    public interface IUserService
    {
        UserInfoViewModel ConvertUserToUserInfoModel(ApplicationUser user);

        ApplicationUser ConvertUserInfoModelToUser(UserInfoViewModel model, ApplicationUser user);

       
    }
}
