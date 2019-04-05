using JobApplication.Data;
using JobApplication.Data.Models;
using System;
using System.Linq;

namespace JobApplication.Services
{
    /// <summary>
    /// This class implements the IUserService interface and is used 
    /// to ensure the user functionalities (To be able to log in, to be able to register in the data base and 
    /// to get the user that is currently logged in)
    /// </summary>
    public class UserService : IUserService
    {
        private JobApplicationDbContext context;

        public UserService(JobApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// This method adds a user with the given parameters in the User table of the database. 
        /// </summary>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="age">age</param>
        /// <param name="email">email</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="confirmPassword">Confirm password</param>
        /// <param name="isEmployer">is the user and employer</param>
        /// <returns>The Id of the registered user</returns>
        public int Register(string firstName,
                            string lastName,
                            int age,
                            string email,
                            string phoneNumber,
                            string username,
                            string password,
                            string confirmPassword,
                            bool isEmployer) {

            bool takenInfo = context.Users.FirstOrDefault(x => x.Username == username) != null 
                || context.Users.FirstOrDefault(x => x.Email == email) != null 
                ||context.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber) != null;

            if (takenInfo)
            {
                return -1;
            }
            var user = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Email = email,
                PhoneNumber = phoneNumber,
                Username = username,
                Password = password,
                ConfirmPassword = confirmPassword,
                IsEmployer = isEmployer
            };

            context.Users.Add(user);
            context.SaveChanges();

            return user.Id;
        }

        /// <summary>
        /// This methods logs in the user
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>This method returns the user Id if it exists in the data base.</returns>
        public int Login(string username, string password)
        {
            var user = context.Users.FirstOrDefault(x => x.Username == username && x.Password == password);

            if (user != null)
            {
                LoggedUserInfo.LoggedUserId = user.Id;
                return user.Id;
            }
            return -1;
        }

        /// <summary>
        /// This method returns the user that is currently logged in.
        /// </summary>
        /// <returns>The logged in user.</returns>
        public User GetLoggedUser()
        {
            var loggedUser = context.Users.FirstOrDefault(x => x.Id == LoggedUserInfo.LoggedUserId);
            if (context.CVs.FirstOrDefault(c => c.UserId == loggedUser.Id) != null)
            {
                return context.CVs.FirstOrDefault(c => c.UserId == loggedUser.Id).User;
            }
            return loggedUser;
        }
    }
}
