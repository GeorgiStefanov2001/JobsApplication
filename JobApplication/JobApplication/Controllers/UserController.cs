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
        public IActionResult Register(string firstName, 
                                      string lastName, 
                                      int age, 
                                      string email, 
                                      string phoneNumber,
                                      string username, 
                                      string password, 
                                      string confirmPassword, 
                                      bool isEmployer) {
            if (ModelState.IsValid)
            {
                if (service.Register(firstName, lastName, age, email, phoneNumber, username, password, confirmPassword, isEmployer) == -1) {
                    ViewBag.Message = "Username/Email or Phone Number already taken!";
                    return View();
                }
            }
            return RedirectToAction("Login", "User");
        }

        public IActionResult Login()
        {
            LoggedUserInfo.LoggedUserId = 0;
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

        public IActionResult Profile()
        {
            ViewData["User"] = service.GetLoggedUser();
            return View();
        }
    }
}
