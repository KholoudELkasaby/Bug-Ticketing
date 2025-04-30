using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public interface IProjectMemberManager
{
    Task<GeneralResult> AddProjectMemberAsync(ProjectMemberAddDto projectMemberAddDto);

    Task<GeneralResult<List<ProjectMemberViewDto>>> GetAllProjectMembersAsync();

    Task<GeneralResult> RemoveProjectMemberAsync(ProjectMemberRemoveDto projectMemberRemoveDto);
    Task<GeneralResult<IEnumerable<ProjectMemberByProjectIdViewDto>>> GetProjectMembersByProjectIdAsync(Guid projectId);

    Task<GeneralResult<IEnumerable<ProjectMemberByUserIdViewDto>>> GetProjectMembersByUserIdAsync(Guid userId);
}
