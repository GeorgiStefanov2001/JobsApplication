using JobApplication.Data;
using JobApplication.Data.Models;
using JobApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace JobApplication.Controllers
{
    public class UserController : Controller
    {
        private IUserService service;
        private User loggedUser;
        private JobApplicationDbContext context;

        public UserController(IUserService service, JobApplicationDbContext context)
        {
            this.service = service;
            this.context = context;
        }

        private void CheckLoggedUser()
        {
            if (LoggedUserInfo.LoggedUserId != 0)
            {
                loggedUser = service.GetLoggedUser();
                ViewData["LoggedUser"] = loggedUser;
            }
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
            var loggedUser = service.GetLoggedUser();
            ViewData["User"] = loggedUser;
            ViewData["UserCv"] = context.CVs.Where(c => c.UserId == loggedUser.Id).FirstOrDefault();
            CheckLoggedUser();
            return View();
        }
    }
}
