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
        private IJobService JobsService;
        private IUserService UserService;
        private User loggedUser;

        public JobsController(IUserService UserService, IJobService JobsService)
        {
            this.JobsService = JobsService;
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
                JobsService.CreateJob(name, salary, category, description, workPlace, requiredExperience, requiredEducation);
            }
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult ViewJob(int id)
        {
            ViewData["Job"] = JobsService.ViewJob(id);

            CheckLoggedUser();
            return View();
        }

        public IActionResult ApplyForJob(int id)
        {
            ViewBag.Message = "Successfully applied for job.";
            if (JobsService.ApplyForJob(id) == -1)
            {
                ViewBag.Message = "You have already applied for this job.";
            }else if(JobsService.ApplyForJob(id) == 0)
            {
                return RedirectToAction("Login", "User");
            }

            ViewData["Job"] = JobsService.ViewJob(id);
            CheckLoggedUser();
            return View("ViewJob");
        }
    }

}