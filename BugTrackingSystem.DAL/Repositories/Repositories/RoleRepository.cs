using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    private readonly BugTrackingSystemContext _context;
    public RoleRepository(BugTrackingSystemContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<User>> GetUsersByRoleAsync(Guid roleId)
    {
        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Where(u => u.UserRoles.Any(ur => ur.RoleId == roleId))
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Role?> GetFirstOrDefaultAsync(Expression<Func<Role, bool>> predicate)
    {
        return await _context.Roles.FirstOrDefaultAsync(predicate);
    }
}
