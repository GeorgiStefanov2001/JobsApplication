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
    /// <summary>
    /// This is the controller for the Job entity.
    /// It inherites the Controller class and provides the actions a certain job has.
    /// </summary>
    public class JobsController : Controller
    {
        private IJobService JobsService;
        private IUserService UserService;
        private User loggedUser;

        /// <summary>
        /// This is the constructor of the JobsController class.
        /// </summary>
        /// <param name="UserService">User service</param>
        /// <param name="JobsService">Job service</param>
        public JobsController(IUserService UserService, IJobService JobsService)
        {
            this.JobsService = JobsService;
            this.UserService = UserService;
        }

        /// <summary>
        /// This method checks if there is any logged user.
        /// If so, the logged user is put in the ViewData which is
        /// passed to the CreateJob view.
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
        /// <returns>The CreateJob view and passes it the ViewData.</returns>
        public IActionResult CreateJob()
        {
            CheckLoggedUser();
            return View();
        }

        /// <summary>
        /// This HttpPost action uses the Job service to 
        /// create a job with the given parameters.
        /// </summary>
        /// <param name="name">Job name</param>
        /// <param name="salary">Job salary</param>
        /// <param name="category">Job category</param>
        /// <param name="description">Job description</param>
        /// <param name="workPlace">Job work place</param>
        /// <param name="requiredExperience">Job's required experience</param>
        /// <param name="requiredEducation">Job's required education</param>
        /// <returns>A RedirectToAction method which redirects the user to the Index view
        /// where he can continue searching for jobs.
        /// </returns>
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

        /// <summary>
        /// This action checks the logged user and puts the job that is going to be viewed in details in the ViewData.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the ViewJob view and passes it the ViewData.</returns>
        public IActionResult ViewJob(int id)
        {
            ViewData["Job"] = JobsService.ViewJob(id);

            CheckLoggedUser();
            return View();
        }

        /// <summary>
        /// This action represents the applying functionaity.
        /// If the user has already applied for the job that he wants to apply
        /// the application tells him that he has already applied and nothing happens.
        /// If the user is not logged in yet but tries to apply for a job, the user is 
        /// redirected to the log in page where he can log in and then continue searching for jobs.
        /// Otherwise (if the user has logged in and wants to apply for a job he has not already), 
        /// that job is put in the ViewData and the methods returns the ViewJob view with a messeage telling
        /// the user that he successfully applied for that job. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Described above.</returns>
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