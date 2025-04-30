


namespace BugTrackingSystem.DAL;

public class UnitWork : IUnitWork
{
    public IProjectRepository ProjectRepository { get; }

    public IProjectMemberRepository ProjectMemberRepository { get; }

    public IBugRepository BugRepository { get; }

    public IUserRepository UserRepository { get; }

    public IUserRoleRepository UserRoleRepository { get; }

    public IRoleRepository RoleRepository { get; }

    public IAttachmentRepository AttachmentRepository { get; }

    public IBugAssignmentRepository BugAssignmentRepository { get; }

    private readonly BugTrackingSystemContext _context;
    public UnitWork(BugTrackingSystemContext context,
        IProjectRepository projectRepository,
        IProjectMemberRepository projectMemberRepository,
        IBugRepository bugRepository,
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        IRoleRepository roleRepository,
        IAttachmentRepository attachmentRepository,
        IBugAssignmentRepository bugAssignmentRepository
        )
    { 
        _context = context;
        ProjectRepository = projectRepository;
        ProjectMemberRepository = projectMemberRepository;
        BugRepository = bugRepository;
        UserRepository = userRepository;
        UserRoleRepository = userRoleRepository;
        RoleRepository = roleRepository;
        AttachmentRepository = attachmentRepository;
        BugAssignmentRepository = bugAssignmentRepository;
    }
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
