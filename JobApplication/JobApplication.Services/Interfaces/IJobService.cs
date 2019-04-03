using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Services.Interfaces
{
    public interface IJobService
    {
        int CreateJob(string name,
                      int salary,
                      string employer,
                      string category,
                      string description,
                      int requiredExperience,
                      string requiredEducation);
    }
}
