using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TomasosPizzeria.Data;
using TomasosPizzeria.Repositories;
using TomasosPizzeria.Services;
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ISessionService sessionService;
        private readonly IOrderRepository orderRepository;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(
            IUserRepository userRepository, 
            ISessionService sessionService, 
            IOrderRepository orderRepository, 
            IUserService userService, 
            UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.sessionService = sessionService;
            this.orderRepository = orderRepository;
            this.userService = userService;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.CreateUser(model);

                var userCreationResult = await userRepository.RegisterUser(user,model);
         
                if (userCreationResult.Succeeded)
                {
                    var roleAssignmentResult = await userRepository.SetUserRole(user, "RegularUser");

                    if (roleAssignmentResult.Succeeded)
                    {
                        await userRepository.SignInUser(model);
                        var customer = await userRepository.GetUser(model);

                        sessionService.SetUser(customer);

                        return RedirectToAction("CustomerHome");
                    }
                    
                }
            }
            return View();
        }

        [AllowAnonymous]
        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userRepository.SignInUser(model);

                if (result.Succeeded)
                {
                    var customer = await userRepository.GetUser(model);
                    sessionService.SetUser(customer);

                    if (await userRepository.IsAdmin(customer))
                    {
                        return RedirectToAction("Index", "Admin");
                    }

                    return RedirectToAction("CustomerHome");
                }

                ModelState.AddModelError(string.Empty, "Fel användarnamn eller lösenord");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            sessionService.ClearSessionData();
            await userRepository.SignOutUser();

            return RedirectToAction("Index","Home");
        }

        [Route("Minsida")]
        [HttpGet]
        public async Task<IActionResult> CustomerHome()
        {
            var model = new AdminOrderViewModel();
            model.User = await userManager.GetUserAsync(User);
            model.Orders = orderRepository.GetAllCustomerOrders(model.User.Id);
            model.UserInfoModel = userService.ConvertUserToUserInfoModel(model.User);

            return View(model);
        }

        [HttpGet]
        public IActionResult OrderInfo(int orderId)
        {
            var model = new OrderDetailsViewModel();

            model.Foods = orderRepository.GetOrderFoodDetails(orderId);
            model.OrderId = orderId;

            return ViewComponent("OrderInfo", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(AdminOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                user = userService.ConvertUserInfoModelToUser(model.UserInfoModel, user);
                var updateUser = await userManager.UpdateAsync(user);
                if (updateUser.Succeeded)
                {
                    return RedirectToAction("CustomerHome");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAccount(AdminOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                user.UserName = model.AccountModel.AnvandarNamn;

                var updateUser = await userManager.UpdateAsync(user);

                if (updateUser.Succeeded && model.AccountModel.NewPassword == null)
                {
                    return RedirectToAction("CustomerHome");
                }
                else
                {
                    var updatePassword = await userManager.ChangePasswordAsync(user, 
                        model.AccountModel.Losenord, model.AccountModel.NewPassword);

                    if (updatePassword.Succeeded)
                    {
                        return RedirectToAction("CustomerHome");
                    }
                }
               
            }
            return View();
        }
    }
}