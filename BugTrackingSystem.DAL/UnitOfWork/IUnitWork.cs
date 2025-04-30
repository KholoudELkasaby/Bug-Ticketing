using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.DAL;

public interface IUnitWork
{
    IProjectRepository ProjectRepository { get; }
    IProjectMemberRepository ProjectMemberRepository { get; }
    IBugRepository BugRepository { get; }
    IUserRepository UserRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }
    IRoleRepository RoleRepository { get; } 
    IAttachmentRepository AttachmentRepository { get; }
    IBugAssignmentRepository BugAssignmentRepository { get; }
    Task<int> SaveChangesAsync();
}
