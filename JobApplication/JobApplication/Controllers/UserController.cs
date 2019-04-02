using JobApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Controllers
{
    public class UserController : Controller
    {
        private IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;    
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost] 
        public IActionResult Register(string firstName, string lastName, int age, string email,
            string username, string password, string confirmPassword)
        {
            service.Register(firstName, lastName, age, email, username, password, confirmPassword);
            return this.RedirectToAction("Index", "Home");
        }
    }
}
