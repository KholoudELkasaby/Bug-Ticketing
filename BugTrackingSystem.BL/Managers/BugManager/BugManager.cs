using BugTrackingSystem.DAL;

namespace BugTrackingSystem.BL;

public class BugManager : IBugManager
{
    private readonly IUnitWork _unitWork;
    private readonly AddBugDtoValidator _addBugDtoValidator;
    private readonly BugUpdateDtoValidator _bugUpdateDtoValidator;
    public BugManager(
        IUnitWork unitWork,
        AddBugDtoValidator addBugDtoValidator,
        BugUpdateDtoValidator bugUpdateDtoValidator
        )
    {
        _unitWork = unitWork;
        _addBugDtoValidator = addBugDtoValidator;
        _bugUpdateDtoValidator = bugUpdateDtoValidator;
    }
    public async Task<GeneralResult> AddBugAsync(BugAddDto bugAddDto)
    {
       var validationResult = await _addBugDtoValidator
            .ValidateAsync(bugAddDto);
        if (!validationResult.IsValid)
        {
            return validationResult.MapErrorToGeneralResult();
        }
        var bug = new Bug
        {
            Title = bugAddDto.Title,
            Description = bugAddDto.Description,
            Status = bugAddDto.Status,
            Priority = bugAddDto.Priority,
            ProjectId = bugAddDto.ProjectId,
        };
        await _unitWork.BugRepository
            .AddAsync(bug);
        await _unitWork.SaveChangesAsync();
        return new GeneralResult
        {
            Success = true,
            Message = "Bug added successfully.",
        };
    }

    public async Task<GeneralResult> GetAllBugsAsync()
    {
        var bugs = await _unitWork.BugRepository
            .GetBugsWithProjectInfo();
        if (bugs == null || !bugs.Any())
        {
            return new GeneralResult
            {
                Success = false,
                Message = "No bugs found."
            };
        }
        var bugViewDtos = bugs.Select(b => new BugViewDto
        {
            Id = b.BugId,
            Title = b.Title,
            Description = b.Description,
            Status = b.Status,
            Priority = b.Priority,
            Project = new ProjectViewForBugDto
            {
                ProjectId = b.Project.ProjectId,
                Name = b.Project.Name,
            }
        }).ToList();
        return new GeneralResult<List<BugViewDto>>
        {
            Success = true,
            Message = "Bugs retrieved successfully.",
            Data = bugViewDtos
        };
    }

    public async Task<GeneralResult> GetBugByIdAsync(Guid bugId)
    {
        var bug = await _unitWork.BugRepository
            .GetBugWithProjectInfo(bugId);
        if (bug == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Bug not found."
            };
        }
        var bugViewDto = new BugViewDto
        {
            Id = bug.BugId,
            Title = bug.Title,
            Description = bug.Description,
            Status = bug.Status,
            Priority = bug.Priority,
            Project = new ProjectViewForBugDto
            {
                ProjectId = bug.Project.ProjectId,
                Name = bug.Project.Name,
            }
        };
        return new GeneralResult<BugViewDto>
        {
            Success = true,
            Message = "Bug retrieved successfully.",
            Data = bugViewDto
        };
    }

    public async Task<GeneralResult> RemoveBugAsync(Guid bugId)
    {
        var bug = await _unitWork.BugRepository
            .GetByIdAsync(bugId);
        if (bug == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Bug not found."
            };
        }
        await _unitWork.BugRepository
            .DeleteAsync(bugId);
        await _unitWork.SaveChangesAsync();
        return new GeneralResult
        {
            Success = true,
            Message = "Bug removed successfully."
        };
    }
    public async Task<GeneralResult> UpdateBugAsync(Guid bugId, BugUpdateDto bugUpdateDto)
    {
        var validationResult = await _bugUpdateDtoValidator
            .ValidateAsync(bugUpdateDto);
        if (!validationResult.IsValid)
        {
            return validationResult.MapErrorToGeneralResult();
        }
        var bug = await _unitWork.BugRepository
            .GetByIdAsync(bugId);
        if (bug == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Bug not found."
            };
        }
        bug.Title = bugUpdateDto.Title;
        bug.Description = bugUpdateDto.Description;
        bug.Status = bugUpdateDto.Status;
        bug.Priority = bugUpdateDto.Priority;
        bug.ProjectId = bugUpdateDto.ProjectId;
        await _unitWork.SaveChangesAsync();
        return new GeneralResult
        {
            Success = true,
            Message = "Bug updated successfully.",
        };
    }
}
