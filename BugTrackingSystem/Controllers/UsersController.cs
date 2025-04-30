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
    public class UsersController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly IUserManager _userManager;
        public UsersController(IAuthManager authManager,
            IUserManager userManager)
        {
            _authManager = authManager;
            _userManager = userManager;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> Login([FromBody] Login model)
        {
            var response = await _authManager.Login(model);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> Register([FromBody] Register model)
        {
            var response = await _authManager.Register(model);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }
        [HttpGet("validate")]
        [Authorize]
        public IActionResult ValidateToken()
        {
            return Ok(new { isValid = true });
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> GetAllUsers()
        {
            var users = await _userManager
                .GetUsersWithRolesAndBugsInfo();
            if (users.Success)
            {
                return TypedResults.Ok(users);
            }
            else
            {
                return TypedResults.BadRequest(users);
            }
        }
        [HttpDelete("{id:guid}")]
        [Authorize(Policy = Constants.Policies.Admin)]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> DeleteUser(Guid id)
        {
            var response = await _userManager.DeleteUserAsync(id);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }
        [HttpPut("{id:guid}")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> UpdateUser(Guid id, [FromBody] UserUpdateDto userUpdateDto)
        {
            var response = await _userManager.UpdateUserAsync(id, userUpdateDto);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }
        [HttpGet("{id:guid}")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> GetUserById(Guid id)
        {
            var response = await _userManager.GetUserAsync(id);
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