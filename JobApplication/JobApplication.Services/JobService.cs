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
    /// <summary>
    /// This is a class implements the IJobService interface and is used
    /// to ensure the functionalities that a certain job has.
    /// </summary>
    public class JobService : IJobService
    {
        private JobApplicationDbContext context;
        private IUserService userService;

        /// <summary>
        /// This is the constructor of the JobService class
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userService"></param>
        public JobService(JobApplicationDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        /// <summary>
        /// This method creates a job with the given parameters and adds it
        /// to the database.
        /// </summary>
        /// <param name="name">Job name</param>
        /// <param name="salary">Job salary</param>
        /// <param name="category">Job category</param>
        /// <param name="description">Job description</param>
        /// <param name="workPlace">Job work place</param>
        /// <param name="requiredExperience">Job required experience</param>
        /// <param name="requiredEducation">Job education</param>
        /// <returns>This method return the job Id.</returns>
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

        /// <summary>
        /// This method returns job according to the AllJobsViewModel.
        /// If we want to show the jobs that the current user has created (if he is an employer)
        /// the method's only parameter is set to true and if we want to show all available jobs, it is set to false.
        /// </summary>
        /// <param name="viewCreatedJobs">view only created jobs</param>
        /// <returns>Aforementioned</returns>
        public AllJobsViewModel GetAllJobs(bool viewCreatedJobs)
        {
            var jobs = context.Jobs.Select(j => new CreateJobViewModel()
            {
                Id = j.Id,
                Name = j.Name,
                Employer = j.Employer,
                Salary = j.Salary,
                Category = j.Category,
                WorkPlace = j.WorkPlace,
                Applicants = j.Applicants
            });

            if (viewCreatedJobs)
            {
                var loggedUser = userService.GetLoggedUser();
                var employer = $"{loggedUser.FirstName} {loggedUser.LastName}";
                jobs = jobs.Where(j => j.Employer == employer);
            }
            var model = new AllJobsViewModel() { Jobs = jobs };

            return model;
        }

        /// <summary>
        /// This method returns the song with the same id as the given one.
        /// </summary>
        /// <param name="id">Job Id</param>
        /// <returns>Aforementioned</returns>
        public Job ViewJob(int id)
        {
            return context.Jobs.Where(j => j.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// This method enables the user to apply for the listed jobs.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If there is not any logged user the method returns 0.
        /// if a user is logged in but tries to apply for a job that he has already applied for, the method returns -1.
        /// if a user is trying to apply for a job he created, the method returns -2.
        /// Eventually if a user is logged in and applies for a job he has not applied yet, the method returns 1;
        /// </returns>
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

            var employer = $"{loggedUser.FirstName} {loggedUser.LastName}";
            if (context.Jobs.Where(j => j.Id == id).FirstOrDefault().Employer == employer) 
            {
                return -2;
            }

            context.Jobs.Where(j => j.Id == id).FirstOrDefault().Applicants.Add(loggedUser);
            context.SaveChanges();
            return 1;
        }
    }
}
