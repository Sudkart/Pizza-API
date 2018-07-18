using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STM.Core.EntityLayer;
using System.Threading.Tasks;


namespace STM.Core.Repositories
{
    public interface IProjectRepository
    {
           Task<bool> CreateProject(Project project);
           Task<bool> UpdateProject(Project project);
           Task<bool> DeleteProject(Project project);
           Task<IQueryable<Project>> GetProjects();
           Task<IQueryable<Project>> GetActiveProjects();
           Task<Project> GetProjectById(int id);

    }
}
