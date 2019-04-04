using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobApplication.Services;
using JobApplication.Services.Interfaces;
using JobApplication.Data.Models;

namespace JobApplication.Controllers
{
    public class JobsController : Controller
    {
        private IJobService service;
        private IUserService UserService;
        private User loggedUser;

        public JobsController(IUserService UserService, IJobService service)
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

        public IActionResult CreateJob()
        {
            CheckLoggedUser();
            return View();
        }

        [HttpPost]
        public IActionResult CreateJob(string name,
                             decimal salary,
                             string category,
                             string description,
                             string workPlace,
                             int requiredExperience,
                             string requiredEducation){
            if (ModelState.IsValid)
            {
                service.CreateJob(name, salary, category, description, workPlace, requiredExperience, requiredEducation);
            }
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult ViewJob(string jobName)
        {
            ViewData["Job"] = service.ViewJob(jobName);

            CheckLoggedUser();
            return View();
        }
    }

}