using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public interface IBugAssignmentManager
{
    Task<GeneralResult> AssignBugToUserAsync(Guid bugId, AssignUserRequestDto assignUserRequestDto);
    Task<GeneralResult> RemoveBugAssignmentAsync(Guid bugId, Guid userId);
}
