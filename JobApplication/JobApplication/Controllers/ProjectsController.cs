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
    public class ProjectsController : Controller
    {
        private IProjectService service;
        private IUserService UserService;
        private User loggedUser;

        public ProjectsController(IUserService UserService, IProjectService service)
        {
            this.service = service;
            this.UserService = UserService;
        }

        private void CheckLoggedUser()
        {
            if (LoggedUserInfo.LoggedUserId != 0)
            {
                loggedUser = UserService.GetLoggedUser();
                ViewData["LoggedUser"] = loggedUser;
            }
        }

        public IActionResult CreateProject()
        {
            CheckLoggedUser();
            return View();
        }

        [HttpPost]
        public IActionResult CreateProject(string name, string technology, string description, string achievedGoals, string futureGoals)
        {
            if (ModelState.IsValid)
            {
                service.CreateProject(name, technology, description, achievedGoals, futureGoals);
            }
            return this.RedirectToAction("Index", "Home");
        }
    }
}