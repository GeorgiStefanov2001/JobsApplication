using JobApplication.Data;
using JobApplication.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Services
{
    public class UserService : IUserService
    {
        private JobApplicationDbContext context;

        public UserService(JobApplicationDbContext context)
        {
            this.context = context;
        }


        public int Register
            (string firstName, string lastName, int age, string email,
            string username, string password, string confirmPassword){

            var user = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Email = email,
                Username = username,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            this.context.Users.Add(user);
            this.context.SaveChanges();

            return user.Id;
        }
    }
}
