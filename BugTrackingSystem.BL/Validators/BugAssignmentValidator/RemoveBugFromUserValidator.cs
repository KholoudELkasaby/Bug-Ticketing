using BugTrackingSystem.DAL;
using FluentValidation;

namespace BugTrackingSystem.BL;

public class RemoveBugFromUserValidator : AbstractValidator<RemoveBugFromUserDto>
{
    private readonly IUnitWork _unitWork;
    public RemoveBugFromUserValidator(
        IUnitWork unitWork
        )
    {
        _unitWork = unitWork;
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");
        RuleFor(x => x.BugId)
            .NotEmpty()
            .WithMessage("Bug ID is required.");
        RuleFor(x => x)
            .MustAsync(ExistingUserAndBug)
            .WithMessage("User does not exist or Bug does not exist.");
        RuleFor(x => x)
            .MustAsync(ExistingBugAssignment)
            .WithMessage("Bug is already Removed from the user.");
    }
    private async Task<bool> ExistingUserAndBug(RemoveBugFromUserDto dto, CancellationToken cancellationToken)
    {
        var user = await _unitWork.UserRepository
            .GetByIdAsync(dto.UserId);
        var bug = await _unitWork.BugRepository
            .GetByIdAsync(dto.BugId);
        return !(user == null || bug == null);
    }
    private async Task<bool> ExistingBugAssignment(RemoveBugFromUserDto dto, CancellationToken cancellationToken)
    {
        var isAssigned = await _unitWork.BugAssignmentRepository
            .IsBugAssignedToUserAsync(dto.BugId, dto.UserId);
        return isAssigned;
    }
}
