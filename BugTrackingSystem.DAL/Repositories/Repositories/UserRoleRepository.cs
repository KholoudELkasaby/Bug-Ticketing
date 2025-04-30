using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL;

public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
{
    private readonly BugTrackingSystemContext _context;
    public UserRoleRepository(BugTrackingSystemContext context)
        : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<UserRole>> GetUserRolesByRoleIdAsync(Guid roleId)
    {
        return await _context.Set<UserRole>()
            .Where(ur => ur.RoleId == roleId)
            .Include(ur => ur.Role)
            .Include(ur => ur.User)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<IEnumerable<UserRole>> GetUserRolesByUserIdAsync(Guid userId)
    {
        return await _context.Set<UserRole>()
            .Where(ur => ur.UserId == userId)
            .Include(ur => ur.Role)
            .Include(ur => ur.User)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task RemoveUserRoleAsync(Guid userId, Guid roleId)
    {
        await _context.Set<UserRole>()
            .Where(ur => ur.UserId == userId && ur.RoleId == roleId)
            .ExecuteDeleteAsync();
    }
    public async Task<bool> ExistsByCompositeKeyAsync(Guid userId, Guid roleId)
    {
        return await _context.Set<UserRole>()
            .AsNoTracking()
            .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
    }
}
