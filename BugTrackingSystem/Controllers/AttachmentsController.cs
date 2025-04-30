using System;
using BugTrackingSystem.BL;
using BugTrackingSystem.DAL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {
        private readonly IAttachmentManager _attachmentManager;
        public AttachmentsController(IAttachmentManager attachmentManager)
        {
            _attachmentManager = attachmentManager;
        }
        [HttpGet]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> GetAllAttachments()
        {
            var response = await _attachmentManager
                .GetAllAttachmentsAsync();
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }
        }

        [HttpGet("{attachmentId:guid}")]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> GetAttachmentById([FromRoute] Guid attachmentId)
        {
            var response = await _attachmentManager
                .GetAttachmentByIdAsync(attachmentId);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }
        }
    }
}
