using BugTrackingSystem.DAL;

namespace BugTrackingSystem.BL;

public class UserRoleManager : IUserRoleManager
{
    private readonly IUnitWork _unitWork;
    private readonly UserRoleAddDtoValidator _userRoleAddDtoValidator;
    private readonly UserRoleDeleteValidator _userRoleDeleteValidator;
    public UserRoleManager(IUnitWork unitWork,
        UserRoleAddDtoValidator userRoleAddDtoValidator,
        UserRoleDeleteValidator userRoleDeleteValidator)
    {
        _unitWork = unitWork;
        _userRoleAddDtoValidator = userRoleAddDtoValidator;
        _userRoleDeleteValidator = userRoleDeleteValidator;
    }

    public async Task<GeneralResult> AddUserRoleAsync(UserRoleAddDto userRoleAddDto)
    {
        var validationResult = await _userRoleAddDtoValidator
            .ValidateAsync(userRoleAddDto);
        if (!validationResult.IsValid)
        {
            return validationResult.MapErrorToGeneralResult();
        }
        var userRole = new UserRole
        {
            UserId = userRoleAddDto.UserId,
            RoleId = userRoleAddDto.RoleId,
            AssignedDate = DateTime.UtcNow
        };
        await _unitWork.UserRoleRepository.AddAsync(userRole);
        await _unitWork.SaveChangesAsync();
        return new GeneralResult
        {
            Success = true,
            Message = "User role added successfully."
        };
    }
    public async Task<GeneralResult<List<UserRoleViewDto>>> GetAllUserRolesAsync()
    {
        var userRoles = await _unitWork.UserRoleRepository
            .GetAllAsync();

        var userRoleViewDtos = userRoles.Select(ur => new UserRoleViewDto
        {
            UserId = ur.UserId,
            RoleId = ur.RoleId,
            AssignedDate = ur.AssignedDate
        }).ToList();

        return new GeneralResult<List<UserRoleViewDto>>
        {
            Success = true,
            Message = "User roles retrieved successfully",
            Data = userRoleViewDtos
        };
    }

    public async Task<GeneralResult> RemoveRoleFromUserAsync(UserRoleRemoveDto userRoleRemoveDto)
    {
        var resultValidation = await _userRoleDeleteValidator
            .ValidateAsync(userRoleRemoveDto);
        if (!resultValidation.IsValid)
        {
            return resultValidation.MapErrorToGeneralResult();
        }
        await _unitWork.UserRoleRepository
        .RemoveUserRoleAsync(userRoleRemoveDto.UserId, userRoleRemoveDto.RoleId);
        await _unitWork.SaveChangesAsync();
        return new GeneralResult
        {
            Success = true,
            Message = "User role removed successfully."
        };
    }
}
