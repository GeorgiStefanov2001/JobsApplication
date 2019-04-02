using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Services
{
    public interface IUserService
    {
        int Register(string firstName, string lastName, int age, string email, string username, string password, string confirmPassword);
    }
}
