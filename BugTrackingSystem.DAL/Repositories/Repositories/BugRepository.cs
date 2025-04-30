using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL;

public class BugRepository : GenericRepository<Bug>, IBugRepository
{
    private readonly BugTrackingSystemContext _context;
    public BugRepository(BugTrackingSystemContext context)
        : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Bug>> GetBugsByAssigneeAsync(Guid userId)
    {
        return await _context.Set<Bug>()
            .Where(b => b.BugAssignments.Any(bs => bs.UserId == userId))
            .Include(b => b.BugAssignments)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Bug>> GetBugsByPriorityAsync(BugPriority priority)
    {
        return await _context.Set<Bug>()
            .Where(b => b.Priority == priority)
            .Include(b => b.BugAssignments)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Bug>> GetBugsByProjectAsync(Guid projectId)
    {
        return await _context.Set<Bug>()
            .Where(b => b.ProjectId == projectId)
            .Include(b => b.Project)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Bug>> GetBugsWithProjectInfo()
    {
        return await _context.Set<Bug>()
            .Include(b => b.Project)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<IEnumerable<Bug>> GetBugsByStatusAsync(BugStatus status)
    {
        return await _context.Set<Bug>()
            .Where(b => b.Status == status)
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<bool> IsBugAssignedToUserAsync(Guid bugId, Guid userId)
    {
        return _context.Set<Bug>()
            .AnyAsync(b => b.BugId == bugId && b.BugAssignments.Any(bs => bs.UserId == userId));
    }
    public async Task<Bug?> GetBugWithProjectInfo(Guid bugId)
    {
        return await _context.Set<Bug>()
            .Include(b => b.Project)
            .FirstOrDefaultAsync(b => b.BugId == bugId);
    }
}
