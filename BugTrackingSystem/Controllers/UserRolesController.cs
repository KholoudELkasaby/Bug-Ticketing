using BugTrackingSystem.BL;
using BugTrackingSystem.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleManager _userRoleManager;
        public UserRolesController(IUserRoleManager userRoleManager)
        {
            _userRoleManager = userRoleManager;
        }
        [HttpPost]
        [Authorize(Policy = Constants.Policies.Admin)]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> AddUserRole([FromBody] UserRoleAddDto userRoleAddDto)
        {
            var response = await _userRoleManager.AddUserRoleAsync(userRoleAddDto);

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

        public async Task<Results<Ok<GeneralResult<List<UserRoleViewDto>>>, NotFound<GeneralResult<List<UserRoleViewDto>>>>>
            GetAllUserRoles()
        {
            var response = await _userRoleManager.GetAllUserRolesAsync();

            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.NotFound(response);
            }
        }
        [HttpDelete]
        [Authorize(Policy = Constants.Policies.Admin)]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> RemoveRoleFromUser([FromBody] UserRoleRemoveDto userRoleRemoveDto)
        {
            var response = await _userRoleManager
                .RemoveRoleFromUserAsync(userRoleRemoveDto);
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
