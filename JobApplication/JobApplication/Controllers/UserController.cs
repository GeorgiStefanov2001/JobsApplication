using JobApplication.Services;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Register(string firstName, string lastName, int age, string email, string username, string password, string confirmPassword)
        {      
            if (ModelState.IsValid)
            {
                service.Register(firstName, lastName, age, email, username, password, confirmPassword);
            }
            return this.RedirectToAction("Login", "User");
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                if (service.Login(username, password) == -1)
                {
                    return RedirectToAction("Login", "User");
                }
            }
            return RedirectToAction("Index","Home");
        }


    }
}
