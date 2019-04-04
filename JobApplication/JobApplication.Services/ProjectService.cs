using JobApplication.Data;
using JobApplication.Data.Models;
using JobApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            User loggeduser = userService.GetLoggedUser();
            var loggedUserCv =  context.CVs.Where(c => c.UserId == loggeduser.Id).FirstOrDefault();
            if (loggedUserCv != null)
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
                context.CVs.Where(c => c.UserId == loggeduser.Id).FirstOrDefault().Projects.Add(project);
                context.SaveChanges();
                return project.Id;
            }
            return -1;
        }
    }
}
