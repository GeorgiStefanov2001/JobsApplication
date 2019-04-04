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
    public class CvsController : Controller
    {
        private IUserService UserService;
        private ICvService service;
        private User loggedUser;

        public CvsController(IUserService UserService, ICvService service)
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

        public IActionResult CreateCv()
        {
            CheckLoggedUser();
            return View();
        }

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

        public IActionResult ViewCv(int id)
        {
            ViewData["CV"] = service.ViewCv(id);
            CheckLoggedUser();
            return View();
        }
    }
}