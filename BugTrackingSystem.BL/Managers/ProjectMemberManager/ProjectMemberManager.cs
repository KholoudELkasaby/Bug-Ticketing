using BugTrackingSystem.DAL;

namespace BugTrackingSystem.BL
{
    public class ProjectMemberManager : IProjectMemberManager
    {
        private readonly IUnitWork _unitWork;
        private readonly ProjectMemberAddDtoValidator _projectMemberAddDtoValidator;
        private readonly ProjectMemberRemoveDtoValidator _projectMemberRemoveDtoValidator;

        public ProjectMemberManager(IUnitWork unitWork,
            ProjectMemberAddDtoValidator projectMemberAddDtoValidator,
            ProjectMemberRemoveDtoValidator projectMemberRemoveDtoValidator)
        {
            _unitWork = unitWork;
            _projectMemberAddDtoValidator = projectMemberAddDtoValidator;
            _projectMemberRemoveDtoValidator = projectMemberRemoveDtoValidator;
        }

        public async Task<GeneralResult> AddProjectMemberAsync(ProjectMemberAddDto projectMemberAddDto)
        {
            var validationResult = await _projectMemberAddDtoValidator.ValidateAsync(projectMemberAddDto);
            if (!validationResult.IsValid)
            {
                return validationResult.MapErrorToGeneralResult();
            }

            var projectMember = new ProjectMember
            {
                ProjectId = projectMemberAddDto.ProjectId,
                UserId = projectMemberAddDto.UserId,
                Notes = projectMemberAddDto.Notes,
                JoinedDate = projectMemberAddDto.JoinedDate
            };

            await _unitWork.ProjectMemberRepository.AddAsync(projectMember);
            await _unitWork.SaveChangesAsync();

            return new GeneralResult
            {
                Success = true,
                Message = "Project member added successfully."
            };
        }

        public async Task<GeneralResult<List<ProjectMemberViewDto>>> GetAllProjectMembersAsync()
        {
            var projectMembers = await _unitWork.ProjectMemberRepository.GetAllAsync();

            var projectMemberViewDtos = projectMembers.Select(pm => new ProjectMemberViewDto
            {
                ProjectId = pm.ProjectId,
                UserId = pm.UserId,
                Notes = pm.Notes,
                JoinedDate = pm.JoinedDate
            }).ToList();

            return new GeneralResult<List<ProjectMemberViewDto>>
            {
                Success = true,
                Message = "Project members retrieved successfully.",
                Data = projectMemberViewDtos
            };
        }

        public async Task<GeneralResult> RemoveProjectMemberAsync(ProjectMemberRemoveDto projectMemberRemoveDto)
        {
            var resultValidation = await _projectMemberRemoveDtoValidator.ValidateAsync(projectMemberRemoveDto);
            if (!resultValidation.IsValid)
            {
                return resultValidation.MapErrorToGeneralResult();
            }

            await _unitWork.ProjectMemberRepository
                .RemoveProjectMemberAsync(projectMemberRemoveDto.ProjectId, projectMemberRemoveDto.UserId);

            await _unitWork.SaveChangesAsync();

            return new GeneralResult
            {
                Success = true,
                Message = "Project member removed successfully."
            };
        }

        public async Task<GeneralResult<IEnumerable<ProjectMemberByProjectIdViewDto>>> GetProjectMembersByProjectIdAsync(Guid projectId)
        {
            var projectMembers = await _unitWork.ProjectMemberRepository.GetProjectMembersByProjectIdAsync(projectId);
            if (projectMembers == null || !projectMembers.Any())
            {
                return new GeneralResult<IEnumerable<ProjectMemberByProjectIdViewDto>>
                {
                    Success = false,
                    Message = "No project members found or project does not exist.",
                    Errors = new List<ResultError>
            {
                new ResultError
                {
                    Message = "Project not found.",
                    Code = "404"
                }
            }
                };
            }

            var projectMemberDtos = projectMembers.Select(pm => new ProjectMemberByProjectIdViewDto
            {
                ProjectId = pm.ProjectId,
                UserId = pm.UserId,
                Notes = pm.Notes,
                JoinedDate = pm.JoinedDate
            });

            return new GeneralResult<IEnumerable<ProjectMemberByProjectIdViewDto>>
            {
                Success = true,
                Message = "Project members for the given project retrieved successfully.",
                Data = projectMemberDtos
            };
        }

        public async Task<GeneralResult<IEnumerable<ProjectMemberByUserIdViewDto>>> GetProjectMembersByUserIdAsync(Guid userId)
        {
            var projectMembers = await _unitWork.ProjectMemberRepository.GetProjectMembersByUserIdAsync(userId);
            if (projectMembers == null || !projectMembers.Any())
            {
                return new GeneralResult<IEnumerable<ProjectMemberByUserIdViewDto>>
                {
                    Success = false,
                    Message = "No projects found for the given user or user does not exist.",
                    Errors = new List<ResultError>
            {
                new ResultError
                {
                    Message = "User not found.",
                    Code = "404"
                }
            }
                };
            }
            var projectMemberDtos = projectMembers.Select(pm => new ProjectMemberByUserIdViewDto
            {
                ProjectId = pm.ProjectId,
                UserId = pm.UserId,
                Notes = pm.Notes,
                JoinedDate = pm.JoinedDate
            });

            return new GeneralResult<IEnumerable<ProjectMemberByUserIdViewDto>>
            {
                Success = true,
                Message = "Projects for the given user retrieved successfully.",
                Data = projectMemberDtos
            };
         }


    }
}
