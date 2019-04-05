using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobApplication.Data.Models;
using JobApplication.Services;
using JobApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Controllers
{
    /// <summary>
    /// This is the controller class for the Project entity.
    /// It inherites the Controller class and provides the actions a certain project has.
    /// </summary>
    public class ProjectsController : Controller
    {
        private IProjectService service;
        private IUserService UserService;
        private User loggedUser;

        /// <summary>
        /// This is the contructor of the ProjectController class
        /// </summary>
        /// <param name="UserService">User service</param>
        /// <param name="service">Project service</param>
        public ProjectsController(IUserService UserService, IProjectService service)
        {
            this.service = service;
            this.UserService = UserService;
        }

        /// <summary>
        /// This method checks if there is any logged user.
        /// If so, the logged user is put in the ViewData which is
        /// passed to the CreateProject view.
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
        /// <returns>The CreateProject view and passes the ViewData to that view.</returns>
        public IActionResult CreateProject()
        {
            CheckLoggedUser();
            return View();
        }

        /// <summary>
        /// This HttpPost action uses the Project service to create a project
        /// with the given parameters.
        /// </summary>
        /// <param name="name">Project name</param>
        /// <param name="technology">Project technology</param>
        /// <param name="description">Project description</param>
        /// <param name="achievedGoals">Project achieved goals</param>
        /// <param name="futureGoals">Project future goals</param>
        /// <returns>A RedirectToAction method which redirects 
        /// the user to the ViewCv view where you can check your CV.
        /// </returns>
        [HttpPost]
        public IActionResult CreateProject(string name, string technology, string description, string achievedGoals, string futureGoals)
        {
            if (ModelState.IsValid)
            {
                service.CreateProject(name, technology, description, achievedGoals, futureGoals);
            }
            return RedirectToAction("ViewCv", "Cvs");
        }
    }
}