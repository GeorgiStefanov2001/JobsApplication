using JobApplication.Data;
using JobApplication.Data.Models;
using JobApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace JobApplication.Controllers
{
    /// <summary>
    /// This is the controller class for the user entity.
    /// It inherites the Controller class and provides the actions a certain user has.
    /// </summary>
    public class UserController : Controller
    {
        private IUserService service;
        private User loggedUser;
        private JobApplicationDbContext context;

        /// <summary>
        /// This is the constructor of the UserController class
        /// </summary>
        /// <param name="service">The User service that this controller will use</param>
        /// <param name="context">Data base context</param>
        public UserController(IUserService service, JobApplicationDbContext context)
        {
            this.service = service;
            this.context = context;
        }

        /// <summary>
        /// This method checks if there is a logged user.
        /// If there is a logged user, takes it and create a ViewData of it.
        /// </summary>
        private void CheckLoggedUser()
        {
            if (LoggedUserInfo.LoggedUserId != 0)
            {
                loggedUser = service.GetLoggedUser();
                ViewData["LoggedUser"] = loggedUser;
            }
        }

        /// <summary>
        /// This action returns the Register view in the User folder.
        /// </summary>
        /// <returns>Aforementioned.</returns>
        public IActionResult Register()
        {
            return this.View();
        }

        /// <summary>
        /// This HttpPost action uses a A UserServiece to create a user and save it in tha database.
        /// </summary>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="age">Age</param>
        /// <param name="email">Email</param>
        /// <param name="phoneNumber"><Phone Number/param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="confirmPassword">Confirm password</param>
        /// <param name="isEmployer">Is the user an employer</param>
        /// <returns>If the user is registered it redirects him to the log in page.</returns>
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

        /// <summary>
        /// This action return the Login view in the User folder.
        /// </summary>
        /// <returns>Aforementioned.</returns>
        public IActionResult Login()
        {
            LoggedUserInfo.LoggedUserId = 0;
            return this.View();
        }

        /// <summary>
        /// This HttpPost action checks if the user has logged in successfully.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>If the user has logged in successfuly, this method redirects him to the index page.
        /// Otherwise, it redirects him back to the log in page.
        /// </returns>
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

        /// <summary>
        /// This action gets the logged user and his Cv passes them to the Profile view 
        /// and returns that view.
        /// </summary>
        /// <returns>The Profile view</returns>
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
