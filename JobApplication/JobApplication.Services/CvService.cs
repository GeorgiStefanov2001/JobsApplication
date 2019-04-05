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
    ///  This is a class implements the ICvService interface and is used
    ///  to ensure the functionalities that a certain cv has.
    /// </summary>
    public class CvService : ICvService
    {
        private JobApplicationDbContext context;
        private IUserService userService;

        /// <summary>
        /// This is the constructor of the ProjectService class
        /// </summary>
        /// <param name="context">Data base context</param>
        /// <param name="userService">User service</param>
        public CvService(JobApplicationDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        /// <summary>
        /// This method creates a cv with the given parameters in the database
        /// </summary>
        /// <param name="education">Education</param>
        /// <param name="experience">Experience</param>
        /// <param name="userId">The id of the user that will has that cv.</param>
        /// <returns>The id of the created cv.</returns>
        public int CreateCv(string education, int experience, int userId)
        {
            User loggedUser = userService.GetLoggedUser();
            var cv = new CV
            {
                Education = education,
                Experience = experience,
                UserId = loggedUser.Id
            };

            context.CVs.Add(cv);
            context.SaveChanges();

            context.CVs.Where(c => c.UserId == loggedUser.Id).FirstOrDefault().User = loggedUser;
            context.SaveChanges();

            return cv.Id;
        }

        /// <summary>
        /// This method returns a Cv that will be used in the ViewCv view then.
        /// </summary>
        /// <returns>Aforementioned.</returns>
        public CV ViewCv()
        {
            return context.CVs.Where(c => c.UserId == LoggedUserInfo.LoggedUserId).FirstOrDefault();
        }
    }
}
