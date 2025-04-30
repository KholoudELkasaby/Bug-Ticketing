using BugTrackingSystem.DAL;

namespace BugTrackingSystem.BL;

public class BugAddDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public BugStatus Status { get; set; }
    public BugPriority Priority { get; set; }
    public Guid ProjectId { get; set; }
}
