﻿using JobApplication.Data;
using JobApplication.Data.Models;
using JobApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Services
{
    public class CvService : ICvService
    {
        private JobApplicationDbContext context;
        private IUserService userService;

        public CvService(JobApplicationDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

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

            return cv.Id;
        }
    }
}
