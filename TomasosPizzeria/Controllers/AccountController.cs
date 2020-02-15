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
        private readonly IOrderRepository orderRepository;

        public AccountController(IUserRepository userRepository, ISessionService sessionService, IOrderRepository orderRepository)
        {
            this.userRepository = userRepository;
            this.sessionService = sessionService;
            this.orderRepository = orderRepository;
        }

        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

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

        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Login")]
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

        [Route("Minsida")]
        [HttpGet]
        public IActionResult CustomerHome()
        {
            var model = new AdminOrderViewModel();
            model.User = sessionService.GetUser();
            model.Orders = orderRepository.GetAllCustomerOrders(model.User.Id);
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
    }
}