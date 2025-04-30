
using BugTrackingSystem.DAL;

namespace BugTrackingSystem.BL;
public class AttachmentManager : IAttachmentManager
{
    private readonly IUnitWork _unitWork;
    private readonly AttachmentUploadDtoValidator _validations;

    public AttachmentManager(IUnitWork unitWork,
        AttachmentUploadDtoValidator validations)
    {
        _unitWork = unitWork;
        _validations = validations;
    }
    public async Task<GeneralResult> SaveAttachmentAsync(AttachmentUploadDto request, Guid bugId)
    {
        var validationResult = await _validations.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return validationResult.MapErrorToGeneralResult();
        }
        var bug = await _unitWork.BugRepository
            .GetByIdAsync(bugId);
        if (bug == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Bug not found"
            };
        }
        var fileName = Guid.NewGuid() + Path.GetExtension(request.File.FileName);
        var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var attachmentsFolder = Path.Combine(webRootPath, "attachments");

        if (!Directory.Exists(attachmentsFolder))
        {
            Directory.CreateDirectory(attachmentsFolder);
        }

        var filePath = Path.Combine(attachmentsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream);
        }

        var fileUrl = $"/attachments/{fileName}";

        var attachment = new Attachment
        {
            FileName = request.File.FileName,
            ContentType = request.File.ContentType,
            FileUrl = fileUrl,
            BugId = bugId
        };

        await _unitWork.AttachmentRepository
            .AddAsync(attachment);
        await _unitWork.SaveChangesAsync();

        return new GeneralResult<AttachmentViewDto>
        {
            Success = true,
            Message = "Attachment uploaded successfully",
            Data = new AttachmentViewDto
            {
                AttachmentId = attachment.AttachmentId,
                FileName = attachment.FileName,
                FilePath = $"http://localhost:5279{fileUrl}",
                CreatedDate = attachment.CreatedDate
            }
        };
    }
    public async Task<GeneralResult> GetAttachmentsByBugIdAsync(Guid bugId)
    {
        var attachments = await _unitWork.AttachmentRepository
            .GetAttachmentsByBugIdAsync(bugId);
        if (attachments == null || !attachments.Any())
        {
            return new GeneralResult
            {
                Success = false,
                Message = "No attachments found for this bug"
            };
        }
        var attachmentDtos = attachments.Select(a => new AttachmentViewDto
        {
            AttachmentId = a.AttachmentId,
            FileName = a.FileName,
            FilePath = $"http://localhost:5279{a.FileUrl}",
            CreatedDate = a.CreatedDate
        }).ToList();
        return new GeneralResult<List<AttachmentViewDto>>
        {
            Success = true,
            Data = attachmentDtos
        };
    }
    public async Task<GeneralResult> DeleteAttachmentByIdAndBugIdAsync(Guid bugId, Guid attachmentId)
    {
        var bug = await _unitWork.BugRepository
            .GetByIdAsync(bugId);
        if (bug == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Bug not found"
            };
        }
        var attachment = await _unitWork.AttachmentRepository
            .GetAttachmentByIdAndBugIdAsync(attachmentId, bugId);
        if (attachment == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Attachment not found"
            };
        }
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", attachment.FileUrl);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        await _unitWork.AttachmentRepository
            .DeleteAsync(attachmentId);
        await _unitWork.SaveChangesAsync();
        return new GeneralResult
        {
            Success = true,
            Message = "Attachment deleted successfully"
        };
    }
    public async Task<GeneralResult> GetAllAttachmentsAsync()
    {
        var attachments = await _unitWork.AttachmentRepository
            .GetAllAsync();
        if (attachments == null || !attachments.Any())
        {
            return new GeneralResult
            {
                Success = false,
                Message = "No attachments found"
            };
        }
        var attachmentDtos = attachments.Select(a => new AttachmentViewDto
        {
            AttachmentId = a.AttachmentId,
            FileName = a.FileName,
            FilePath = $"http://localhost:5279{a.FileUrl}",
            CreatedDate = a.CreatedDate
        }).ToList();
        return new GeneralResult<List<AttachmentViewDto>>
        {
            Success = true,
            Data = attachmentDtos
        };
    }

    public async Task<GeneralResult> GetAttachmentByIdAsync(Guid attachmentId)
    {
        var attachment = await _unitWork.AttachmentRepository
            .GetByIdAsync(attachmentId);
        if (attachment == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Attachment not found"
            };
        }
        var attachmentDto = new AttachmentViewDto
        {
            AttachmentId = attachment.AttachmentId,
            FileName = attachment.FileName,
            FilePath = $"http://localhost:5279{attachment.FileUrl}",
            CreatedDate = attachment.CreatedDate
        };
        return new GeneralResult<AttachmentViewDto>
        {
            Success = true,
            Data = attachmentDto
        };
    }
}


