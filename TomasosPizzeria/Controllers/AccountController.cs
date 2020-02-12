using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class AccountController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ISessionService sessionService;

        public AccountController(IUserRepository userRepository, ISessionService sessionService)
        {
            this.userRepository = userRepository;
            this.sessionService = sessionService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await userRepository.SignInUser(model);

            if (result.Succeeded)
            {
                var customer = await userRepository.GetUser(model);
                sessionService.SetUser(customer);

                if (await userRepository.IsAdmin(customer))
                {
                    return RedirectToAction("Index","Admin");
                }

                return RedirectToAction("CustomerHome");
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

        [HttpGet]
        public IActionResult CustomerHome()
        {
            var model = sessionService.GetUser();
            return View(model);
        }
    }
}