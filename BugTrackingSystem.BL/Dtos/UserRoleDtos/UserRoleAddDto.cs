
namespace BugTrackingSystem.BL;

public class UserRoleAddDto
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
}
