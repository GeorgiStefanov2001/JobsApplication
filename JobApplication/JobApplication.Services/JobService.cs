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
        private IUserService userService;

        public JobService(JobApplicationDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public int CreateJob(string name, 
                             decimal salary, 
                             string category, 
                             string description, 
                             string workPlace,
                             int requiredExperience, 
                             string requiredEducation){

            var loggedUser = userService.GetLoggedUser(); 

            var job = new Job
            {
                Name = name,
                Salary = salary,
                Employer = loggedUser.FirstName + " " + loggedUser.LastName, 
                EmployerPhoneNumber = loggedUser.PhoneNumber,
                Category = category,
                Description = description,
                WorkPlace = workPlace,
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
                Id = j.Id,
                Name = j.Name,
                Salary = j.Salary,
                Category = j.Category,
                WorkPlace = j.WorkPlace
            });

            var model = new AllJobsViewModel() { Jobs = jobs };

            return model;
        }

        public Job ViewJob(int id)
        {
            return context.Jobs.Where(j => j.Id == id).FirstOrDefault();
        }

        public int ApplyForJob(int id)
        {
            if (LoggedUserInfo.LoggedUserId == 0)
            {
                return 0; //we need to log in
            }

            var loggedUser = userService.GetLoggedUser();

            if (context.Jobs.Where(j => j.Id == id).FirstOrDefault().Applicants.Contains(loggedUser))
            {
                return -1;
            }
            context.Jobs.Where(j => j.Id == id).FirstOrDefault().Applicants.Add(loggedUser);
            context.SaveChanges();
            return 1;
        }
    }
}
