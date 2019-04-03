using JobApplication.Data;
using JobApplication.Data.Models;
using JobApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Services
{
    public class JobService : IJobService
    {
        private JobApplicationDbContext context;

        public JobService(JobApplicationDbContext context)
        {
            this.context = context;
        }

        public int CreateJob(string name, 
                             int salary, 
                             string employer, 
                             string category, 
                             string description, 
                             int requiredExperience, 
                             string requiredEducation){

            var job = new Job
            {
                Name = name,
                Salary = salary,
                Employer = employer,
                Category = category,
                Description = description,
                RequiredExperience = requiredExperience,
                RequiredEducation = requiredEducation
            };

            context.Jobs.Add(job);
            context.SaveChanges();

            return job.Id;
        }
    }
}
