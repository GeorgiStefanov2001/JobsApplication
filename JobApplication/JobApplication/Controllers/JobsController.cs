using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobApplication.Services;
using JobApplication.Services.Interfaces;

namespace JobApplication.Controllers
{
    public class JobsController : Controller
    {
        private IJobService service;

        public JobsController(IJobService service)
        {
            this.service = service;
        }

        public IActionResult CreateJob()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateJob(string name,
                             int salary,
                             string employer,
                             string category,
                             string description,
                             int requiredExperience,
                             string requiredEducation){
            if (ModelState.IsValid)
            {
                service.CreateJob(name, salary, employer, category, description, requiredExperience, requiredEducation);
            }
            return this.RedirectToAction("Index", "Home");
        }
    }
}