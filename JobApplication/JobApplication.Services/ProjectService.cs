using JobApplication.Data;
using JobApplication.Data.Models;
using JobApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobApplication.Services
{
    /// <summary>
    /// This class implements the IProjectService interface and is used to
    /// ensure the functionalities that a certain project has (to create a project)
    /// </summary>
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

        /// <summary>
        /// this method creates a project with the given parameters
        /// </summary>
        /// <param name="name">Project name</param>
        /// <param name="technology">Project technologies</param>
        /// <param name="description">Project description</param>
        /// <param name="achievedGoals">Project achieved goals</param>
        /// <param name="futureGoals">Project future goals</param>
        /// <returns>The user that is creating the project has a Cv the method returns the project Id.
        /// Otherwise, the method returns -1.
        /// </returns>
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
