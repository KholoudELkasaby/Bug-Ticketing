
namespace BugTrackingSystem.BL;

public interface IRoleManager
{
    Task<GeneralResult> AddRoleAsync(RoleAddDto roleAddDto);
    Task<GeneralResult<List<RoleViewDto>>> GetAllRolesAsync();
    Task<GeneralResult> UpdateRoleAsync(Guid id, RoleUpdateDto roleUpdateDto);
    Task<GeneralResult> DeleteRoleAsync(Guid id);
    Task<GeneralResult<List<RoleViewDto>>> GetRoleByIdAsync(Guid id);
}
