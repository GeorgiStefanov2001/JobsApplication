﻿using JobApplication.Data.Models;
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
                      string workPlace,
                      int requiredExperience,
                      string requiredEducation);

        AllJobsViewModel GetAllJobs(bool viewCreatedJobs);

        Job ViewJob(int id);

        int ApplyForJob(int id);

    }

}

