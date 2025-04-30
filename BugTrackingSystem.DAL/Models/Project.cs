namespace BugTrackingSystem.DAL
{
    public class Project
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProjectStatus Status { get; set; } = ProjectStatus.InProgress;
        public DateTime? StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; } = DateTime.UtcNow.AddYears(1);
        public bool IsActive { get; set; } = true;
        public ICollection<Bug> Bugs { get; set; } = new HashSet<Bug>();
        public ICollection<ProjectMember> ProjectMembers { get; set; } = new HashSet<ProjectMember>();
    }
}
