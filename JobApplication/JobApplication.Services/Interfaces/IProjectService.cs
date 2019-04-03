using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Services.Interfaces
{
    public interface IProjectService
    {
        int CreateProject(string name, string technology, string description, string achievedGoals, string futureGoals);
    }
}
