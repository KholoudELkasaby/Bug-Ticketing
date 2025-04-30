using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.DAL;

public class AttachmentRepository : GenericRepository<Attachment>, IAttachmentRepository
{
    private readonly BugTrackingSystemContext _context;
    public AttachmentRepository(BugTrackingSystemContext context) 
        : base(context)
    {
        _context = context;
    }
    public async Task<Attachment?> GetAttachmentByIdAndBugIdAsync(Guid attachmentId, Guid bugId)
    {
        return await _context.Set<Attachment>()
            .Include(a => a.Bug)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AttachmentId == attachmentId && a.BugId == bugId)
            ?? null;
    }
    public async Task<List<Attachment>?> GetAttachmentsByBugIdAsync(Guid bugId)
    {
        return await _context.Set<Attachment>()
            .Include(a => a.Bug)
            .Where(a => a.BugId == bugId)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Attachment?> GetAttachmentByFilePathAsync(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));
        }

        return await _context.Set<Attachment>()
            .FirstOrDefaultAsync(a => a.FileUrl == filePath);
    }
    public async Task<bool> DeleteAttachmentAsync(Guid bugId, Guid attachmentId)
    {
        var attachment = await _context.Attachments
        .FirstOrDefaultAsync(a => a.AttachmentId == attachmentId && a.BugId == bugId);

        if (attachment == null)
        {
            return false;
        }

        _context.Attachments.Remove(attachment);
        return true;
    }
    public async Task<Attachment?> UploadAttachmentAsync(Guid bugId, Attachment attachment)
    {

        var bug = await _context.Bugs
            .FirstOrDefaultAsync(b => b.BugId == bugId);
        if (bug == null)
        {
            return null;
        }
        attachment.BugId = bugId;

        await _context.Attachments.AddAsync(attachment);
        return attachment;
    }
}
