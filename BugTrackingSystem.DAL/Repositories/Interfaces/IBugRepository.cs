using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL;

public interface IBugRepository : IGenericRepository<Bug>
{
    Task<IEnumerable<Bug>> GetBugsByStatusAsync(BugStatus status);
    Task<IEnumerable<Bug>> GetBugsByPriorityAsync(BugPriority priority);
    Task<IEnumerable<Bug>> GetBugsByAssigneeAsync(Guid userId);
    Task<IEnumerable<Bug>> GetBugsByProjectAsync(Guid projectId);
    Task<bool> IsBugAssignedToUserAsync(Guid bugId, Guid userId);
    Task<List<Bug>> GetBugsWithProjectInfo();
    Task<Bug?> GetBugWithProjectInfo(Guid bugId);
}
