using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL;

public interface IUserRepository: IGenericRepository<User>
{
    Task<List<User>> GetUsersWithRolesInfo();
    Task<List<User>?> GetUsersWithRolesAndBugsInfo();
    Task<IEnumerable<User>> GetActiveUsersAsync();
    Task<bool> IsEmailUniqueAsync(string email);
    Task<bool> IsUserInProjectAsync(Guid userId, Guid projectId);
    Task<User?> GetUserByIdWithAllInfo(Guid id);
}
