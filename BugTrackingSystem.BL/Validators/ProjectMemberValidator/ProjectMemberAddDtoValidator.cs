using BugTrackingSystem.DAL;
using FluentValidation;

namespace BugTrackingSystem.BL
{
    public class ProjectMemberAddDtoValidator : AbstractValidator<ProjectMemberAddDto>
    {
        private readonly IUnitWork _unitWork;

        public ProjectMemberAddDtoValidator(
            IUnitWork unitWork
            )
        {
            _unitWork = unitWork;

            RuleFor(x => x.ProjectId)
                .NotEmpty()
                .WithMessage("Project ID is required.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required.");

            RuleFor(x => x)
                .MustAsync(ValidateProjectAndUserExist)
                .WithMessage("Either the project or user does not exist.");

            RuleFor(x => x)
                .MustAsync(ValidateUserNotAlreadyMember)
                .WithMessage("This user is already a member of the project.");

            RuleFor(x => x.JoinedDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Assigned date cannot be in the future.");
        }

        private async Task<bool> ValidateProjectAndUserExist(ProjectMemberAddDto dto, CancellationToken cancellationToken)
        {
            var project = await _unitWork.ProjectRepository.GetByIdAsync(dto.ProjectId);
            var user = await _unitWork.UserRepository.GetByIdAsync(dto.UserId);
            return !(project == null || user == null);
        }

        private async Task<bool> ValidateUserNotAlreadyMember(ProjectMemberAddDto dto, CancellationToken cancellationToken)
        {
            var exists = await _unitWork.ProjectMemberRepository
                .ExistsByCompositeKeyAsync(dto.ProjectId, dto.UserId);
            return !exists;
        }
    }
}
