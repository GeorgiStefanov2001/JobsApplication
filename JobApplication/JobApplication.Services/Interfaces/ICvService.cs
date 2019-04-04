using JobApplication.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Services.Interfaces
{
    public interface ICvService
    {
        int CreateCv(string education, int experience, int userId);
        CV ViewCv();
    }
}
