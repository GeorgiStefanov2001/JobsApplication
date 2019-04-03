﻿using JobApplication.Data;
using JobApplication.Data.Models;
using JobApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

            context.Users.FirstOrDefault(u => u.Id == loggedUser.Id).UserCv = new CV
            {
                Education = cv.Education,
                Experience = cv.Experience,
                UserId = loggedUser.Id
            };
            context.SaveChanges();

            cv.User = new User
            {
                FirstName = loggedUser.FirstName,
                LastName = loggedUser.LastName,
                Age = loggedUser.Age,
                Email = loggedUser.Email,
                Username = loggedUser.Username,
                Password = loggedUser.Password,
                ConfirmPassword = loggedUser.ConfirmPassword,
                IsEmployer = loggedUser.IsEmployer
            };

            context.CVs.Add(cv);
            context.SaveChanges();

            return cv.Id;
        }
    }
}