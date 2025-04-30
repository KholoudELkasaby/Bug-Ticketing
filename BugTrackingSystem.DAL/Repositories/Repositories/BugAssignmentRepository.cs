using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL;

public class BugAssignmentRepository : GenericRepository<BugAssignment>, IBugAssignmentRepository
{
    private readonly BugTrackingSystemContext _context;
    public BugAssignmentRepository(BugTrackingSystemContext context) 
        : base(context)
    {
        _context = context;
    }
    public async Task AssignBugToUserAsync(BugAssignment bugAssignment)
    {
         await _context.Set<BugAssignment>()
            .AddAsync(bugAssignment);
    }

    public async Task<IEnumerable<BugAssignment>> GetBugAssignmentsByBugIdAsync(Guid bugId)
    {
        return await _context.Set<BugAssignment>()
            .Where(x => x.BugId == bugId)
            .Include(x => x.Bug)
            .Include(x => x.User)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<BugAssignment>?> GetBugAssignmentsByUserIdAsync(Guid userId)
    {
        return await _context.Set<BugAssignment>()
            .Where(x => x.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<List<BugAssignment>?> GetUserAssignmentsByBugIdAsync(Guid bugId)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveBugAssignmentAsync(Guid bugId, Guid userId)
    {
        await _context.Set<BugAssignment>()
            .Where(x => x.BugId == bugId && x.UserId == userId)
            .ExecuteDeleteAsync();
    }
    public async Task<bool> IsBugAssignedToUserAsync(Guid bugId, Guid userId)
    {
        return await _context.Set<BugAssignment>()
            .AnyAsync(x => x.BugId == bugId && x.UserId == userId);
    }
}
