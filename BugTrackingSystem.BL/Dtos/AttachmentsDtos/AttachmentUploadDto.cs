using Microsoft.AspNetCore.Http;

namespace BugTrackingSystem.BL;

public class AttachmentUploadDto
{
    public IFormFile File { get; set; } = null!;
}
