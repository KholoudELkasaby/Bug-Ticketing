using BugTrackingSystem.DAL;
using FluentValidation;

namespace BugTrackingSystem.BL;

public class UserRoleDeleteValidator : AbstractValidator<UserRoleRemoveDto>
{
    private readonly IUnitWork _unitWork;
    public UserRoleDeleteValidator(
        IUnitWork unitWork
        )
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
            .WithMessage("This user already has deleted from this role.");
    }
    private async Task<bool> ValidateUserAndRoleExist(UserRoleRemoveDto dto, CancellationToken cancellationToken)
    {
        var user = await _unitWork.UserRepository
            .GetByIdAsync(dto.UserId);
        var role = await _unitWork.RoleRepository
            .GetByIdAsync(dto.RoleId);
        return !(user == null || role == null);
    }

    private async Task<bool> ValidateUserRoleNotDuplicate(UserRoleRemoveDto dto, CancellationToken cancellationToken)
    {
        var exists = await _unitWork.UserRoleRepository
            .ExistsByCompositeKeyAsync(dto.UserId, dto.RoleId);
        return exists;
    }
}
