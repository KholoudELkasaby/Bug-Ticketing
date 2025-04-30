using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL;

public interface IRoleRepository : IGenericRepository<Role>
{
    Task<IEnumerable<User>> GetUsersByRoleAsync(Guid roleId);
    Task<Role?> GetFirstOrDefaultAsync(Expression<Func<Role, bool>> predicate);
}
