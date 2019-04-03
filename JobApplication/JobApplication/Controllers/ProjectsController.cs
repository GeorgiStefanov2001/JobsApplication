using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Controllers
{
    public class ProjectsController : Controller
    {
        private IProjectService service;

        public ProjectsController(IProjectService service)
        {
            this.service = service;
        }

        public IActionResult CreateProject()
        {
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