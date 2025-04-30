using BugTrackingSystem.DAL;
using FluentValidation;

namespace BugTrackingSystem.BL
{
    public class ProjectMemberRemoveDtoValidator : AbstractValidator<ProjectMemberRemoveDto>
    {
        private readonly IUnitWork _unitWork;

        public ProjectMemberRemoveDtoValidator(IUnitWork unitWork)
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
                .MustAsync(ValidateUserIsMember)
                .WithMessage("This user is not a member of the project or has already been removed.");
        }

        private async Task<bool> ValidateProjectAndUserExist(ProjectMemberRemoveDto dto, CancellationToken cancellationToken)
        {
            var project = await _unitWork.ProjectRepository.GetByIdAsync(dto.ProjectId);
            var user = await _unitWork.UserRepository.GetByIdAsync(dto.UserId);
            return project != null && user != null;
        }

        private async Task<bool> ValidateUserIsMember(ProjectMemberRemoveDto dto, CancellationToken cancellationToken)
        {
            var exists = await _unitWork.ProjectMemberRepository.ExistsByCompositeKeyAsync(dto.ProjectId, dto.UserId);
            return exists;
        }
    }
}
