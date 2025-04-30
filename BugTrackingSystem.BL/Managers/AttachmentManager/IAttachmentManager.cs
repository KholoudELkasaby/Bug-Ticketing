
namespace BugTrackingSystem.BL; 

public interface IAttachmentManager
{

    Task<GeneralResult> SaveAttachmentAsync(AttachmentUploadDto request, Guid bugId);
    Task<GeneralResult> GetAttachmentsByBugIdAsync(Guid bugId);
    Task<GeneralResult> DeleteAttachmentByIdAndBugIdAsync(Guid bugId, Guid attachmentId);
    Task<GeneralResult> GetAllAttachmentsAsync();
    Task<GeneralResult> GetAttachmentByIdAsync(Guid attachmentId);
}
