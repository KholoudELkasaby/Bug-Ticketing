using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL;

public class ProjectMemberRepository : GenericRepository<ProjectMember>, IProjectMemberRepository
{
    private readonly BugTrackingSystemContext _context;
    public ProjectMemberRepository(BugTrackingSystemContext context) : base(context)
    {
        _context = context;
    }
    public async Task RemoveProjectMemberAsync(Guid projectId, Guid userId)
    {
        var projectMember = await _context.Set<ProjectMember>()
            .Where(pm => pm.ProjectId == projectId && pm.UserId == userId)
            .ExecuteDeleteAsync();
    }
    public async Task<IEnumerable<ProjectMember>> GetProjectMembersByProjectIdAsync(Guid projectId)
    {
        return await _context.Set<ProjectMember>()
            .Where(pm => pm.ProjectId == projectId)
            .Include(pm => pm.Project)
            .Include(pm => pm.User)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<IEnumerable<ProjectMember>> GetProjectMembersByUserIdAsync(Guid userId)
    {
        return await _context.Set<ProjectMember>()
            .Where(pm => pm.UserId == userId)
            .Include(pm => pm.Project)
            .Include(pm => pm.User)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> ExistsByCompositeKeyAsync(Guid projectId, Guid userId)
    {
        return await _context.Set<ProjectMember>()
            .AsNoTracking()
            .AnyAsync(pm => pm.ProjectId == projectId && pm.UserId == userId);
    }
}
