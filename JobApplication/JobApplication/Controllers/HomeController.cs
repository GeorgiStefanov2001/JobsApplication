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

namespace JobApplication.Controllers
{
    public class HomeController : Controller
    {
        private IUserService service;

        public HomeController(IUserService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            if (LoggedUserInfo.LoggedUserId==0)
            {
                return View("~/Views/User/Login.cshtml");
            }
            User loggedUser = service.GetLoggedUser();
            return View(loggedUser);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            User loggedUser = service.GetLoggedUser();
            return View(loggedUser);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            User loggedUser = service.GetLoggedUser();
            return View(loggedUser);
        }

        public IActionResult Privacy()
        {
            User loggedUser = service.GetLoggedUser();
            return View(loggedUser);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
