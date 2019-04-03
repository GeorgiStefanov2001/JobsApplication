using JobApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Services.Interfaces
{
    public interface IJobService
    {
        int CreateJob(string name,
                      decimal salary,
                      string category,
                      string description,
                      int requiredExperience,
                      string requiredEducation);

        AllJobsViewModel GetAllJobs();
    }
}
