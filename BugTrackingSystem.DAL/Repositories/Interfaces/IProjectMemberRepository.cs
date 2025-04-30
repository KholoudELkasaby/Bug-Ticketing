namespace BugTrackingSystem.DAL;

public interface IProjectMemberRepository : IGenericRepository<ProjectMember>
{
    Task RemoveProjectMemberAsync(Guid projectId, Guid userId);
    Task<IEnumerable<ProjectMember>> GetProjectMembersByProjectIdAsync(Guid projectId);
    Task<IEnumerable<ProjectMember>> GetProjectMembersByUserIdAsync(Guid userId);
    Task<bool> ExistsByCompositeKeyAsync(Guid projectId, Guid userId);

}
