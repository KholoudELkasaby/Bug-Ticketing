using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL;

public interface IProjectRepository: IGenericRepository<Project>
{
    Task<IEnumerable<Project>> GetProjectsByUserIdAsync(Guid userId);
    Task<IEnumerable<Project>> GetProjectsByStatusAsync(ProjectStatus status);
    Task<IEnumerable<Project>> GetProjectsByUserIdAndStatusAsync(Guid userId, ProjectStatus status);
    Task<Project> GetProjectByNameAsync(string projectName);
    Task<bool> IsUserInProjectAsync(Guid userId, Guid projectId);
    Task<IEnumerable<Project>> GetProjectsWithBugsAsync();
    Task<Project> GetProjectWithBugsAsync(Guid projectId);
    Task<List<Project>> GetProjectsWithAllInfoAsync();
    Task<Project> GetProjectWithAllInfoAsync(Guid projectId);
}
