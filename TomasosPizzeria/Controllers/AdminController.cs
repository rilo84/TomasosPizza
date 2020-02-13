using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeria.Data;
using TomasosPizzeria.ViewModels;
using TomasosPizzeria.Repositories;
using TomasosPizzeria.Services;

namespace TomasosPizzeria.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrderRepository orderRepository;
        private readonly IUserRepository userRepository;
        private readonly IFoodRepository foodRepository;
        private readonly ISelectService selectService;

        public AdminController(
            RoleManager<IdentityRole> roleManager, 
            UserManager<ApplicationUser> userManager, 
            IOrderRepository orderRepository, 
            IUserRepository userRepository, 
            IFoodRepository foodRepository,
            ISelectService selectService )
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.orderRepository = orderRepository;
            this.userRepository = userRepository;
            this.foodRepository = foodRepository;
            this.selectService = selectService;
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

        [HttpGet]
        public IActionResult ManageOrders()
        {
            var model = new AdminOrderViewModel();

            model.Users = userManager.Users.ToList();
            

            return View(model);
        }

        [HttpGet]
        public IActionResult ManageFoods()
        {

            var model = new FoodManageViewModel();

            model.Foods = foodRepository.GetAllFoods();

            model.CategorySelectList = selectService.GetListCategory();
            model.FoodSelectList = selectService.GetListFoods();
            model.IngredientSelectList = selectService.GetListIngredients();

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
        public async Task<IActionResult> UserDetails(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);

            return ViewComponent("UserDetails", user);
        }

        [HttpGet]
        public IActionResult OrderDetails(AdminOrderViewModel model)
        {
            if (model.UserId == null)
            {
                model.Orders = orderRepository.GetAllOrders();
                model.Customers = userRepository.GetAllUsers();
            }
            else
                model.Orders = orderRepository.GetAllCustomerOrders(model.UserId);
            
            return ViewComponent("OrderDetails", model);
        }

        [HttpGet]
        public IActionResult OrderInfo(int orderId)
        {
            var model = new OrderDetailsViewModel();

            model.Foods = orderRepository.GetOrderFoodDetails(orderId);
            model.OrderId = orderId;

            return ViewComponent("OrderInfo", model);
        }

        [HttpGet]
        public IActionResult SetOrderDelivered(AdminOrderViewModel model)
        {
            
            orderRepository.SetDelivered(model.OrderId);

            return RedirectToAction("OrderDetails", model);
        }

        [HttpGet]
        public IActionResult SetOrderUndelivered(AdminOrderViewModel model)
        {

            orderRepository.SetUndelivered(model.OrderId);

            return RedirectToAction("OrderDetails", model);
        }


        [HttpGet]
        public IActionResult RemoveOrder(AdminOrderViewModel model)
        {

            orderRepository.RemoveOrder(model.OrderId);

            return RedirectToAction("OrderDetails", model);
        }



    }
}