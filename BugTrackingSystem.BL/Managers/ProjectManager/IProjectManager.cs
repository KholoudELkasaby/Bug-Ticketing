namespace BugTrackingSystem.BL;
using BugTrackingSystem.DAL;

public interface IProjectManager
{
    Task<GeneralResult> AddProjectAsync(ProjectAddDto projectAddDto);
    Task<GeneralResult<List<ProjectViewDto>>> GetAllProjectsAsync();
    Task<GeneralResult> GetProjectByIdAsync(Guid id);
    Task<GeneralResult> UpdateProjectAsync(Guid id, ProjectUpdateDto projectUpdateDto);
    Task<GeneralResult> DeleteProjectAsync(Guid id);
}
