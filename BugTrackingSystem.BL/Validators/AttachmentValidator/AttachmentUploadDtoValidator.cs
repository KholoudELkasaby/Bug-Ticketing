using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.BL;

public class AttachmentUploadDtoValidator : AbstractValidator<AttachmentUploadDto>
{
    public AttachmentUploadDtoValidator()
    {
        RuleFor(x => x.File)
            .NotNull()
            .WithMessage("File is required.");
        RuleFor(x => x.File.Length)
            .LessThanOrEqualTo(5 * 1024 * 1024)
            .WithMessage("File size cannot exceed 5MB")
            .When(x => x.File != null);
        RuleFor(x => x.File.ContentType)
            .Must(x => x.StartsWith("image/") ||
               x.Equals("application/pdf") ||
               x.Equals("application/msword") ||
               x.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document") ||
               x.Equals("application/vnd.ms-excel") ||
               x.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") ||
               x.Equals("text/plain"))
            .WithMessage("Only images, PDFs, Word documents, Excel files, and text files are allowed")
            .When(x => x.File != null);
    }
}
