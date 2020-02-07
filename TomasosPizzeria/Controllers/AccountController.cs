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
using TomasosPizzeria.ViewModels;

namespace TomasosPizzeria.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository userRepository;

        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]

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

                        var customerJson = JsonConvert.SerializeObject(customer);
                        HttpContext.Session.SetString("customerData", customerJson);

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
                var customerJson = JsonConvert.SerializeObject(customer);
                HttpContext.Session.SetString("customerData", customerJson);

                return RedirectToAction("CustomerHome");
            }

            return View();
        }

        [HttpGet]
        public IActionResult CustomerHome()
        {
            var customerJson = HttpContext.Session.GetString("customerData");
            var model = JsonConvert.DeserializeObject<ApplicationUser>(customerJson);

            return View(model);
        }
    }
}