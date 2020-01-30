using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TomasosPizzeria.Models;
using TomasosPizzeria.Repositories;

namespace TomasosPizzeria.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IActionResult NewCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewCustomer(Kund customer)
        {
            _customerRepository.AddCustomer(customer);

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Kund customer)
        {
            var existingCustomer = _customerRepository.LoginCustomer(customer);

            if (existingCustomer != null)
            {
                var customerJson = JsonConvert.SerializeObject(existingCustomer);
                HttpContext.Session.SetString("customerData", customerJson);
                return RedirectToAction("CustomerPage");
            }

            return View();
        }

        public IActionResult CustomerPage()
        {
            var customerJson = HttpContext.Session.GetString("customerData");
            var model = JsonConvert.DeserializeObject<Kund>(customerJson);

            return View(model);
        }
    }
}