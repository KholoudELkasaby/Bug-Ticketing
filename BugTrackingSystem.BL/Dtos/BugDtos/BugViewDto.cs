using BugTrackingSystem.DAL;

namespace BugTrackingSystem.BL;

public class BugViewDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public BugStatus Status { get; set; }
    public BugPriority Priority { get; set; }
    public ProjectViewForBugDto Project { get; set; } = null!;
}
