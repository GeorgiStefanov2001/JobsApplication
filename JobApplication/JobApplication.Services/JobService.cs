using JobApplication.Data;
using JobApplication.Data.Models;
using JobApplication.Services.Interfaces;
using JobApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobApplication.Services
{
    public class JobService : IJobService
    {
        private JobApplicationDbContext context;
        private IUserService service;

        public JobService(JobApplicationDbContext context, IUserService service)
        {
            this.context = context;
            this.service = service;
        }

        public int CreateJob(string name, 
                             decimal salary, 
                             string category, 
                             string description, 
                             int requiredExperience, 
                             string requiredEducation){

            var loggedUser = service.GetLoggedUser();

            var job = new Job
            {
                Name = name,
                Salary = salary,
                Employer = loggedUser.FirstName + " " + loggedUser.LastName, 
                Category = category,
                Description = description,
                RequiredExperience = requiredExperience,
                RequiredEducation = requiredEducation
            };

            context.Jobs.Add(job);
            context.SaveChanges();

            return job.Id;
        }

        public AllJobsViewModel GetAllJobs()
        {
            var jobs = context.Jobs.Select(j => new CreateJobViewModel()
            {
                Name = j.Name,
                Salary = j.Salary,
                Category = j.Category,
                Description = j.Description,
                RequiredExperience = j.RequiredExperience,
                RequiredEducation = j.RequiredEducation
            });

            var model = new AllJobsViewModel() { Jobs = jobs };

            return model;
        }
    }
}
