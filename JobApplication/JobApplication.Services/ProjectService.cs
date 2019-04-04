using JobApplication.Data;
using JobApplication.Data.Models;
using JobApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Services
{
    public class ProjectService : IProjectService
    {
        private JobApplicationDbContext context;
        private IUserService userService;
        private ICvService cvService;

        public ProjectService(JobApplicationDbContext context, IUserService userService, ICvService cvService)
        {
            this.context = context;
            this.userService = userService;
            this.cvService = cvService;
        }

        public int CreateProject(string name, string technology, string description, string achievedGoals, string futureGoals)
        {
            User loggedUser = userService.GetLoggedUser();

            if (loggedUser.UserCv != null)
            {
                var project = new Project
                {
                    Name = name,
                    Technology = technology,
                    Description = description,
                    AchievedGoals = achievedGoals,
                    FutureGoals = futureGoals
                };

                context.Projects.Add(project);
                loggedUser.UserCv.Projects.Add(project);
                context.SaveChanges();
                return project.Id;
            }
            return -1;
        }
    }
}
