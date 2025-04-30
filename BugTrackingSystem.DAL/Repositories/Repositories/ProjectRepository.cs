using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL;

public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    private readonly BugTrackingSystemContext _context;
    public ProjectRepository(BugTrackingSystemContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Project>> GetProjectsByUserIdAndStatusAsync(Guid userId, ProjectStatus status)
    {
        return await _context.Set<Project>()
            .Include(p => p.ProjectMembers)
            .Where(p => p.ProjectMembers.Any(pm => pm.UserId == userId) && p.Status == status)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Project> GetProjectByNameAsync(string projectName)
    {
        return await _context.Set<Project>()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Name == projectName) ?? throw new Exception("Project not found");
    }

    public async Task<IEnumerable<Project>> GetProjectsByStatusAsync(ProjectStatus status)
    {
        return await _context.Set<Project>()
            .Where(p=> p.Status == status)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Project>> GetProjectsByUserIdAsync(Guid userId)
    {
        return await _context.Set<Project>()
             .Include(p => p.ProjectMembers)
             .Where(p => p.ProjectMembers.Any(pm => pm.UserId == userId))
             .AsNoTracking()
             .ToListAsync();
    }

    public async Task<IEnumerable<Project>> GetProjectsWithBugsAsync()
    {
       return await _context.Set<Project>()
            .Include(p => p.Bugs)
            .Where(p => p.Bugs.Any())
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Project> GetProjectWithBugsAsync(Guid projectId)
    {
        return await _context.Set<Project>()
            .Include(p => p.Bugs)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProjectId == projectId) ?? throw new Exception("Project not found");
    }

    public async Task<bool> IsUserInProjectAsync(Guid userId, Guid projectId)
    {
        return await _context.Set<Project>()
            .Include(p => p.ProjectMembers)
            .AnyAsync(p => p.ProjectId == projectId && p.ProjectMembers.Any(pm => pm.UserId == userId));
    }

    public async Task<List<Project>> GetProjectsWithAllInfoAsync()
    {
        return await _context.Set<Project>()
            .Include(p => p.ProjectMembers)
            .ThenInclude(pm => pm.User)
            .Include(p => p.Bugs)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Project> GetProjectWithAllInfoAsync(Guid projectId)
    {
        return await _context.Set<Project>()
            .Include(p => p.ProjectMembers)
            .ThenInclude(pm => pm.User)
            .Include(p => p.Bugs)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProjectId == projectId) ?? throw new Exception("Project not found");
    }
}
