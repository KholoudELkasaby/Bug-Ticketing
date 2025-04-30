using BugTrackingSystem.DAL;
using FluentValidation;

namespace BugTrackingSystem.BL;

public class BugUpdateDtoValidator : AbstractValidator<BugUpdateDto>
{
    private IUnitWork _unitWork;
    public BugUpdateDtoValidator(IUnitWork unitWork)
    {
        _unitWork = unitWork;
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(100)
            .WithMessage("Title must not exceed 100 characters.");
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters.");
        RuleFor(x => x.Priority)
            .IsInEnum()
            .WithMessage("Priority must be a valid enum value.");
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Status must be a valid enum value.");
        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage("ProjectId is required.")
            .MustAsync(BeValidProjectId)
            .WithMessage("ProjectId is not found.");
    }
    private async Task<bool> BeValidProjectId(Guid projectId, CancellationToken cancellationToken)
    {
        var project = await _unitWork.ProjectRepository
            .GetByIdAsync(projectId);
        return project != null;
    }
}
