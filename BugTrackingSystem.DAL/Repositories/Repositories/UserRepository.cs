using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.DAL;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly BugTrackingSystemContext _context;
    public UserRepository(BugTrackingSystemContext context)
        : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<User>> GetActiveUsersAsync()
    {
        return await _context.Users
            .Where(u => u.IsActive)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> GetUserByIdWithAllInfo(Guid id)
    {
        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<User>?> GetUsersWithRolesAndBugsInfo()
    {
        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(u => u.BugAssignments)
            .ThenInclude(ba => ba.Bug)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<User>> GetUsersWithRolesInfo()
    {
        return  await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return await _context.Users
            .AnyAsync(u => u.Email == email);
    }
    public async Task<bool> IsUserInProjectAsync(Guid userId, Guid projectId)
    {
        return await _context.Set<ProjectMember>()
            .AnyAsync(pm => pm.UserId == userId && pm.ProjectId == projectId);
    }
}
