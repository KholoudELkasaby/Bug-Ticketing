using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BugTrackingSystem.DAL;


public class BugTrackingSystemContext:  IdentityDbContext<User, Role, Guid,
    IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public DbSet<Bug> Bugs {  get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<BugAssignment> BugAssignments { get; set; }
    public DbSet<ProjectMember> ProjectMembers { get; set; }

    public BugTrackingSystemContext(DbContextOptions<BugTrackingSystemContext> options)
        : base(options) {}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(BugTrackingSystemContext).Assembly
            );
    }
}
