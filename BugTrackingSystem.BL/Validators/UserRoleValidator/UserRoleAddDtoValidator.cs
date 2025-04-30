using BugTrackingSystem.DAL;
using FluentValidation;

namespace BugTrackingSystem.BL;

public class UserRoleAddDtoValidator : AbstractValidator<UserRoleAddDto>
{
    private readonly IUnitWork _unitWork;

    public UserRoleAddDtoValidator(
        IUnitWork unitWork)
    {
        _unitWork = unitWork;

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");

        RuleFor(x => x.RoleId)
            .NotEmpty()
            .WithMessage("Role ID is required.");

        RuleFor(x => x)
            .MustAsync(ValidateUserAndRoleExist)
            .WithMessage("Either the user or role does not exist.");

        RuleFor(x => x)
            .MustAsync(ValidateUserRoleNotDuplicate)
            .WithMessage("This user already has the specified role assigned.");

        RuleFor(x => x.AssignedDate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Assigned date cannot be in the future.");
    }

    private async Task<bool> ValidateUserAndRoleExist(UserRoleAddDto dto, CancellationToken cancellationToken)
    {
        var user = await _unitWork.UserRoleRepository
            .GetByIdAsync(dto.UserId);
        var role = await _unitWork.RoleRepository
            .GetByIdAsync(dto.RoleId);
        return user != null && role != null;
    }

    private async Task<bool> ValidateUserRoleNotDuplicate(UserRoleAddDto dto, CancellationToken cancellationToken)
    {
        var exists = await _unitWork.UserRoleRepository
            .ExistsByCompositeKeyAsync(dto.UserId, dto.RoleId);
        return !exists; 
    }
}
