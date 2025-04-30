using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL;

public interface IBugAssignmentRepository : IGenericRepository<BugAssignment>
{
    Task AssignBugToUserAsync(BugAssignment bugAssignment);
    Task RemoveBugAssignmentAsync(Guid bugId, Guid userId);
    Task<List<BugAssignment>?> GetBugAssignmentsByUserIdAsync(Guid userId);
    Task<List<BugAssignment>?> GetUserAssignmentsByBugIdAsync(Guid bugId);
    Task<bool> IsBugAssignedToUserAsync(Guid bugId, Guid userId);
}
