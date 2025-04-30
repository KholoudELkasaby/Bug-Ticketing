using BugTrackingSystem.BL;
using BugTrackingSystem.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectMembersController : ControllerBase
    {
        private readonly IProjectMemberManager _projectMemberManager;
        public ProjectMembersController(IProjectMemberManager projectMemberManager)
        {
            _projectMemberManager = projectMemberManager;
        }
        [HttpPost]
        [Authorize(Policy = Constants.Policies.Admin)]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> AddProjectMember([FromBody] ProjectMemberAddDto projectMemberAddDto)
        {
            var result = await _projectMemberManager
                .AddProjectMemberAsync(projectMemberAddDto);

            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.BadRequest(result);
            }
        }


        [HttpDelete]
        [Authorize(Policy = Constants.Policies.Admin)]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> RemoveProjectMember([FromBody] ProjectMemberRemoveDto projectMemberRemoveDto)
        {
            var result = await _projectMemberManager.RemoveProjectMemberAsync(projectMemberRemoveDto);

            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.BadRequest(result);
            }
        }



        [HttpGet]
        public async Task<Results<Ok<GeneralResult<List<ProjectMemberViewDto>>>, NotFound<GeneralResult<List<ProjectMemberViewDto>>>>>
           GetAllProjectMembers()
        {
            var result = await _projectMemberManager.GetAllProjectMembersAsync();

            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.NotFound(result);
            }
        }

        [HttpGet("by-project/{projectId}")]
        public async Task<Results<Ok<GeneralResult<IEnumerable<ProjectMemberByProjectIdViewDto>>>, NotFound<GeneralResult<IEnumerable<ProjectMemberByProjectIdViewDto>>>>>
            GetProjectMembersByProjectId(Guid projectId)
        {
            var result = await _projectMemberManager.GetProjectMembersByProjectIdAsync(projectId);

            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.NotFound(result);
            }
        }

        [HttpGet("by-user/{userId}")]
        public async Task<Results<Ok<GeneralResult<IEnumerable<ProjectMemberByUserIdViewDto>>>, NotFound<GeneralResult<IEnumerable<ProjectMemberByUserIdViewDto>>>>>
            GetProjectMembersByUserId(Guid userId)
        {
            var result = await _projectMemberManager.GetProjectMembersByUserIdAsync(userId);

            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.NotFound(result);
            }
        }
    }
}
