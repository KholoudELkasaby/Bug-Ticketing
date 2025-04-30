using Microsoft.AspNetCore.Http;

namespace BugTrackingSystem.BL;

public class AttachmentAddDto
{

    public Guid AttachmentId { get; set; } = Guid.NewGuid();
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public Guid BugId { get; set; }
}
