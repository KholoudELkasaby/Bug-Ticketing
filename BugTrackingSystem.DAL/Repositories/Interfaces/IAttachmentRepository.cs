

using System.Net.Mail;

namespace BugTrackingSystem.DAL;

public interface IAttachmentRepository: IGenericRepository<Attachment>
{
    
    Task<Attachment?> GetAttachmentByIdAndBugIdAsync(Guid attachmentId, Guid bugId);
    Task<Attachment?> GetAttachmentByFilePathAsync(string filePath);
    Task<Attachment?> UploadAttachmentAsync(Guid bugId, Attachment attachment);
    Task<List<Attachment>?> GetAttachmentsByBugIdAsync(Guid bugId);
    Task<bool> DeleteAttachmentAsync(Guid bugId, Guid attachmentId);
}

