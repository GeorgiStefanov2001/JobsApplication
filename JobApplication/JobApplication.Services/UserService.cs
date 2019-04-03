using JobApplication.Data;
using JobApplication.Data.Models;
using System.Linq;

namespace JobApplication.Services
{

    public class UserService : IUserService
    {
        private JobApplicationDbContext context;

        public UserService(JobApplicationDbContext context)
        {
            this.context = context;
        }


        public int Register(string firstName, 
                            string lastName, 
                            int age, 
                            string email, 
                            string username, 
                            string password, 
                            string confirmPassword, 
                            bool isEmployer)
        {

            var user = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Email = email,
                Username = username,
                Password = password,
                ConfirmPassword = confirmPassword,
                IsEmployer = isEmployer
            };

            context.Users.Add(user);
            context.SaveChanges();

            return user.Id;
        }

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

        public User GetLoggedUser()
        {
            foreach(var user in context.Users)
            {
                System.Console.WriteLine(user.Id);
            }
            var loggedUser = context.Users.FirstOrDefault(x => x.Id == LoggedUserInfo.LoggedUserId);
            return loggedUser;
        }
    }
}
