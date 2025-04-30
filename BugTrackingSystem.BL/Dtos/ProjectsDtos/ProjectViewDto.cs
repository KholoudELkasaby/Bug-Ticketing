using BugTrackingSystem.DAL;


namespace BugTrackingSystem.BL;

public class ProjectViewDto
{
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }
    public List<UserViewInProjectInfo?> Users { get; set; } = null!;
    public List<BugViewForProjectInfo?> Bugs { get; set; } = null!;
}
