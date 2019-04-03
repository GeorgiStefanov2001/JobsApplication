using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobApplication.Models;
using JobApplication.Services;
using JobApplication.Data;
using JobApplication.Data.Models;
using JobApplication.Services.Interfaces;

namespace JobApplication.Controllers
{
    public class HomeController : Controller
    {
        private IUserService UserService;
        private IJobService JobsService;

        public HomeController(IUserService UserService, IJobService jobsService)
        {
            this.UserService = UserService;
            this.JobsService = jobsService;
        }

        public IActionResult Index()
        {
            if (LoggedUserInfo.LoggedUserId==0)
            {
                return RedirectToAction("Login", "User");
            }

            User loggedUser = UserService.GetLoggedUser();
            var allJobs = JobsService.GetAllJobs();

            ViewData["LoggedUser"] = loggedUser;
            ViewData["AllJobs"] = allJobs;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            User loggedUser = UserService.GetLoggedUser();
            ViewData["LoggedUser"] = loggedUser;
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            User loggedUser = UserService.GetLoggedUser();
            ViewData["LoggedUser"] = loggedUser;
            return View();
        }

        public IActionResult Privacy()
        {
            User loggedUser = UserService.GetLoggedUser();
            ViewData["LoggedUser"] = loggedUser;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
