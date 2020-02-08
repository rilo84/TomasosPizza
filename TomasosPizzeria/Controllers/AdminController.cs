using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Data;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManageRoles()
        {
            var model = new RoleViewModel();

            model.Roles = roleManager.Roles.ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult ManageUsers()
        {
            var model = new UserViewModel();
            
            model.Users = userManager.Users.ToList();
            model.Roles = roleManager.Roles.ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole { Name = model.Name };

                var result = await roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ManageRoles");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRole(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.User);

                var currentRoles = await userManager.GetRolesAsync(user);
                var role = currentRoles.FirstOrDefault();
                var removeResult = await userManager.RemoveFromRoleAsync(user,role);
                var addResult = await userManager.AddToRoleAsync(user, model.Role);

                if (removeResult.Succeeded && addResult.Succeeded)
                {
                    return RedirectToAction("Manageusers");
                }
            }

            return RedirectToAction("Manageusers");
        }

        [HttpGet]
        public async Task<IActionResult> UserDetails(string User)
        {
            var user = await userManager.FindByIdAsync(User);

            return ViewComponent("UserDetails", user);
        }
    }
}