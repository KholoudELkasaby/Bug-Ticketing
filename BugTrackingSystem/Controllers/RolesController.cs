using BugTrackingSystem.BL;
using BugTrackingSystem.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleManager _roleManager;
        public RolesController(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<Results<Ok<GeneralResult<List<RoleViewDto>>>, NotFound<GeneralResult<List<RoleViewDto>>>>>
             GetAllRoles()
        {
            var response = await _roleManager.GetAllRolesAsync();

            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }
        }


        [HttpPost]
        [Authorize(Policy = Constants.Policies.Admin)]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> AddRole([FromBody] RoleAddDto roleAddDto)
        {
            var response = await _roleManager.AddRoleAsync(roleAddDto);

            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }

        [HttpGet("{id}")]
        public async Task<Results<Ok<GeneralResult<List<RoleViewDto>>>, NotFound<GeneralResult<List<RoleViewDto>>>>>
        GetRoleById(Guid id)
        {
            var response = await _roleManager.GetRoleByIdAsync(id);

            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Constants.Policies.Admin)]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> UpdateRole(Guid id, [FromBody] RoleUpdateDto roleUpdateDto)
        {
            var response = await _roleManager.UpdateRoleAsync(id, roleUpdateDto);

            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Constants.Policies.Admin)]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> DeleteRole(Guid id)
        {
            var response = await _roleManager.DeleteRoleAsync(id);

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
