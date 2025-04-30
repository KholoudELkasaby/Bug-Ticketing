
namespace BugTrackingSystem.BL;

public class RemoveBugFromUserDto 
{
    public Guid UserId { get; set; }
    public Guid BugId { get; set; }
}
