using BugTrackingSystem.DAL;

namespace BugTrackingSystem.BL;

public class ProjectManager : IProjectManager
{
    private readonly IUnitWork _unitWork;
    private readonly ProjectAddDtoValidator _projectAddDtoValidator;
    private readonly ProjectUpdateDtoValidator _projectUpdateDtoValidator;
    public ProjectManager(IUnitWork unitWork,
        ProjectAddDtoValidator projectAddDtoValidator,
        ProjectUpdateDtoValidator projectUpdateDtoValidator)
    {
        _unitWork = unitWork;
        _projectAddDtoValidator = projectAddDtoValidator;
        _projectUpdateDtoValidator = projectUpdateDtoValidator;
    }
    public async Task<GeneralResult> AddProjectAsync(ProjectAddDto projectAddDto)
    {
        var validationResult = await _projectAddDtoValidator
            .ValidateAsync(projectAddDto);
        if (!validationResult.IsValid)
        {
            return validationResult.MapErrorToGeneralResult();
        }
        var projectAddDb = new Project
        {
            Name = projectAddDto.Name,
            Description = projectAddDto.Description,
            Status = projectAddDto.Status,
            StartDate = projectAddDto.StartDate,
            EndDate = projectAddDto.EndDate,
            IsActive = projectAddDto.IsActive
        };
        await _unitWork.ProjectRepository
            .AddAsync(projectAddDb);
        await _unitWork.SaveChangesAsync();
        return new GeneralResult<ProjectViewDto>
        {
            Success = true,
            Message = "Project added successfully",
            Data = projectAddDb.MapToProjectViewDto()
        };
    }

    public async Task<GeneralResult> DeleteProjectAsync(Guid id)
    {
        var projectFromDb = await _unitWork.ProjectRepository
            .GetByIdAsync(id);
        if (projectFromDb == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Project not found",
                Errors = new List<ResultError>
                {
                    new ResultError
                    {
                        Message = "Project not found",
                        Code = "404"
                    }
                }
            };
        }
        await _unitWork.ProjectRepository.DeleteAsync(id);
        await _unitWork.SaveChangesAsync();
        return new GeneralResult
        {
            Success = true,
            Message = "Project deleted successfully"
        };
    }

    public async Task<GeneralResult<List<ProjectViewDto>>> GetAllProjectsAsync()
    {
        var projectsFromDb = await _unitWork.ProjectRepository
            .GetProjectsWithAllInfoAsync();
        if (projectsFromDb == null || !projectsFromDb.Any())
        {
            return new GeneralResult<List<ProjectViewDto>>
            {
                Success = false,
                Message = "No projects found",
                Errors = new List<ResultError>
                {
                    new ResultError
                    {
                        Message = "No projects found",
                        Code = "404"
                    }
                }
            };
        }
        return new GeneralResult<List<ProjectViewDto>>
        {
            Success = true,
            Message = "Projects retrieved successfully",
            Data = projectsFromDb.Select(p => p.MapToProjectViewDto()).ToList()
        };

        }

    public async Task<GeneralResult> GetProjectByIdAsync(Guid id)
    {
        var projectFromDb = await _unitWork.ProjectRepository
            .GetProjectWithAllInfoAsync(id);
        if (projectFromDb == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Project not found",
                Errors = new List<ResultError>
                {
                    new ResultError
                    {
                        Message = "Project not found",
                        Code = "404"
                    }
                }
            };
        }
        return new GeneralResult<ProjectViewDto>
        {
            Success = true,
            Message = "Projects found",
            Data = projectFromDb.MapToProjectViewDto()
        };
    }

    public async Task<GeneralResult> UpdateProjectAsync(Guid id, ProjectUpdateDto projectUpdateDto)
    {
        var projectFromDb = await _unitWork.ProjectRepository
            .GetByIdAsync(id);
        if (projectFromDb == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Project not found",
                Errors = new List<ResultError>
                {
                    new ResultError
                    {
                        Message = "Project not found",
                        Code = "404"
                    }
                }
            };
        }
        var validationResult = await _projectUpdateDtoValidator
            .ValidateAsync(projectUpdateDto);
        if (!validationResult.IsValid)
        {
            return validationResult.MapErrorToGeneralResult();
        }
        projectFromDb.Name = projectUpdateDto.Name;
        projectFromDb.Description = projectUpdateDto.Description;
        projectFromDb.Status = projectUpdateDto.Status;
        projectFromDb.StartDate = projectUpdateDto.StartDate;
        projectFromDb.EndDate = projectUpdateDto.EndDate;
        projectFromDb.IsActive = projectUpdateDto.IsActive;
        await _unitWork.SaveChangesAsync();
        return new GeneralResult<ProjectViewDto>
        {
            Success = true,
            Message = "Project updated successfully",
            Data = projectFromDb.MapToProjectViewDto()
        };
    }
}
