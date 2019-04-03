using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Controllers
{
    public class CvsController : Controller
    {
        private ICvService service;

        public CvsController(ICvService service)
        {
            this.service = service;
        }

        public IActionResult CreateCv()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCv(string education, int experience, int userId)
        {
            if (ModelState.IsValid)
            {
                service.CreateCv(education, experience, userId);
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}