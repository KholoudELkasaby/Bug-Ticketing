using BugTrackingSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public interface IUserManager
{
    Task<List<UserViewDto>> GetAllUsersAsync();
    Task<GeneralResult> DeleteUserAsync(Guid id);
    Task<GeneralResult> UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto);
    Task<GeneralResult> GetUserAsync(Guid id);
    Task<GeneralResult> GetUsersWithRolesAndBugsInfo();

}
