namespace BugTrackingSystem.BL;

public class AttachmentViewDto
{
    public Guid AttachmentId { get; set; } = Guid.NewGuid();
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
