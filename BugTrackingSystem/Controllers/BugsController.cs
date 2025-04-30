using BugTrackingSystem.BL;
using BugTrackingSystem.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : ControllerBase
    {
        private readonly IBugManager _bugManager;
        private readonly IAttachmentManager _attachmentManager;
        private readonly IBugAssignmentManager _bugAssignmentManager;
        public BugsController(IBugManager bugManager,
            IAttachmentManager attachmentManager,
            IBugAssignmentManager bugAssignmentManager)
        {
            _bugManager = bugManager;
            _attachmentManager = attachmentManager;
            _bugAssignmentManager = bugAssignmentManager;
        }
        [HttpPost]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> AddBug([FromBody] BugAddDto bugAddDto)
        {
            var response = await _bugManager
                .AddBugAsync(bugAddDto);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }
        [HttpGet]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> GetAllBugs()
        {
            var response = await _bugManager
                .GetAllBugsAsync();
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }
        [HttpDelete("{bugId}")]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> RemoveBug(Guid bugId)
        {
            var response = await _bugManager
                .RemoveBugAsync(bugId);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }
        }
        [HttpGet("{bugId}")]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> GetBugById(Guid bugId)
        {
            var response = await _bugManager
                .GetBugByIdAsync(bugId);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }
        }
        [HttpPut("{bugId:guid}")]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> UpdateBug([FromRoute] Guid bugId, [FromBody] BugUpdateDto bugUpdateDto)
        {
            var response = await _bugManager
                .UpdateBugAsync(bugId, bugUpdateDto);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }
        }
        [HttpPost("{bugId:guid}/attachments")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> UploadAttachment([FromRoute] Guid bugId, [FromForm] AttachmentUploadDto dto)
        {
            var response = await _attachmentManager
                .SaveAttachmentAsync(dto, bugId);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }
        [HttpGet("{bugId:guid}/attachments")]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> GetAttachmentsByBugId([FromRoute] Guid bugId)
        {
            var response = await _attachmentManager
                .GetAttachmentsByBugIdAsync(bugId);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }
        }
        [HttpDelete("{bugId:guid}/attachments/{attachmentId:guid}")]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> DeleteAttachment([FromRoute] Guid bugId, [FromRoute] Guid attachmentId)
        {
            var response = await _attachmentManager
                .DeleteAttachmentByIdAndBugIdAsync(bugId, attachmentId);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }
        }
        [HttpPost("{bugId:guid}/assignees")]
        [Authorize(Policy = Constants.Policies.Admin)]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> AssignUserToBug([FromRoute] Guid bugId, [FromBody] AssignUserRequestDto assignUserRequestDto)
        {
            var response = await _bugAssignmentManager
                .AssignBugToUserAsync(bugId, assignUserRequestDto);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }
        [HttpDelete("{bugId:guid}/assignees/{userId:guid}")]
        [Authorize(Policy = Constants.Policies.Admin)]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> RemoveUserFromBug([FromRoute] Guid bugId, [FromRoute] Guid userId)
        {
            var response = await _bugAssignmentManager
                .RemoveBugAssignmentAsync(bugId, userId);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }

    }
}
