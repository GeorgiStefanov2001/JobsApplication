using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobApplication.Data;
using JobApplication.Data.Models;
using JobApplication.Services;
using JobApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Controllers
{
    /// <summary>
    /// This is the controller for the CV entity.
    /// It inherites the Controller class and provides the actions a certain cv has.
    /// </summary>
    public class CvsController : Controller
    {
        private IUserService UserService;
        private ICvService service;
        private User loggedUser;
        private JobApplicationDbContext context;

        /// <summary>
        /// This is the constructor of the CvsController class.
        /// </summary>
        /// <param name="UserService">User service</param>
        /// <param name="service">Cv service</param>
        /// <param name="context">Data base context</param>
        public CvsController(IUserService UserService, ICvService service, JobApplicationDbContext context)
        {
            this.service = service;
            this.UserService = UserService;
            this.context = context;
        }

        /// <summary>
        /// This method checks if there is any logged user.
        /// If so, the logged user is put in the ViewData which is
        /// passed to the Index, Contact, About and Provacy views.
        /// </summary>
        private void CheckLoggedUser()
        {
            if (LoggedUserInfo.LoggedUserId != 0)
            {
                loggedUser = UserService.GetLoggedUser();
                ViewData["LoggedUser"] = loggedUser;
            }
        }

        /// <summary>
        /// This action checks the logged user.
        /// </summary>
        /// <returns>The CreateCv view where the user can create his CV.</returns>
        public IActionResult CreateCv()
        {
            CheckLoggedUser();
            return View();
        }

        /// <summary>
        /// This HttpPost action uses the Cv service to create
        /// a cv with the given parameters.
        /// </summary>
        /// <param name="education">Education</param>
        /// <param name="experience">Experience</param>
        /// <param name="userId">The Id of the user with that cv</param>
        /// <returns>A RedirectToAction method which redirects the user to the Profile page.</returns>
        [HttpPost]
        public IActionResult CreateCv(string education, int experience, int userId)
        {
            if (ModelState.IsValid)
            {
                service.CreateCv(education, experience, userId);
            }

            CheckLoggedUser();
            return this.RedirectToAction("Profile", "User");
        }

        /// <summary>
        /// This action takes the user cv and then puts it in the ViewData.
        /// Finally it puts the data base context to the ViewData.
        /// </summary>
        /// <returns>The ViewCv view which uses the objects in the ViewData.</returns>
        public IActionResult ViewCv()
        {
            var userCv = service.ViewCv();
            ViewData["UserCv"] = userCv;
            ViewData["Context"] = context;
            CheckLoggedUser();
            return View();
        }
    }
}