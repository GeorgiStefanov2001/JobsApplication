using System;
using System.Collections.Generic;
using System.Text;
using JobApplication.Data.Models;

namespace JobApplication.Services
{
    public interface IUserService
    {
        int Register(string firstName, string lastName, int age, string email, string username, string password, string confirmPassword);
        int Login(string username, string password);
        User GetLoggedUser();
    }
}
