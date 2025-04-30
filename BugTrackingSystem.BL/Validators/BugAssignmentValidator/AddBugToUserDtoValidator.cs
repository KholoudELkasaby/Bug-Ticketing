using BugTrackingSystem.DAL;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace BugTrackingSystem.BL;

public class AddBugToUserDtoValidator : AbstractValidator<AddBugToUserDto>
{
    private readonly IUnitWork _unitWork;
    public AddBugToUserDtoValidator(
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
        RuleFor(x => x.AssignedDate)
            .NotEmpty()
            .WithMessage("Assigned date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Assigned date cannot be in the future.");
        RuleFor(x => x)
            .MustAsync(ExistingUserAndBug)
            .WithMessage("User or Bug does not exist.");
        RuleFor(x => x)
            .MustAsync(ExistingBugAssignment)
            .WithMessage("Bug is already Assigned to the user.");
    }
    private async Task<bool> ExistingUserAndBug(AddBugToUserDto dto, CancellationToken cancellationToken)
    {
        var user = await _unitWork.UserRepository
            .GetByIdAsync(dto.UserId);
        var bug = await _unitWork.BugRepository
            .GetByIdAsync(dto.BugId);
        return !(user == null || bug == null);
    }
    private async Task<bool> ExistingBugAssignment(AddBugToUserDto dto, CancellationToken cancellationToken)
    {
        var isAssigned = await _unitWork.BugAssignmentRepository
            .IsBugAssignedToUserAsync(dto.BugId, dto.UserId);
        return !isAssigned;
    }

}
