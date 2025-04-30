using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public interface IUserRoleManager
{
    Task<GeneralResult> AddUserRoleAsync(UserRoleAddDto userRoleAddDto);
    Task<GeneralResult<List<UserRoleViewDto>>> GetAllUserRolesAsync();
    Task<GeneralResult> RemoveRoleFromUserAsync(UserRoleRemoveDto userRoleRemoveDto);
}
