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


        public int Register(string firstName, string lastName, int age, string email, string username, string password, string confirmPassword){

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
    }
}
